---
layout: post
author: tristan-rhodes
title: LLMs as Pathfinding Through Text
excerpt: Ok... hear me out.
featured-image: /assets/llms-as-pathfinding-through-text/featured-image.jpg
---

I've [mentioned my first program was a Chess game](blogging-in-an-ai-world.html). I've also had a long standing passion for exploration and [pathfinding projects](https://github.com/pragmatic-systems/Pragmatic.Playground.Pathfinding) and I've covered [NLP / Text processing and Tokenization in depth](https://github.com/pragmatic-systems/Pragmatic.Playground.Parsing) for over half a decade.

This gives me a solid grounding in two concepts, firstly exploring a domain and prioritising available moves/actions, secondly tokenising words/phrases into concepts based on local context. 

These two concepts underpin a lot of LLM behaviour and I'm going to use the exloration mechanism used in pathfinding and domain discovery as a lens for viewing an LLMs meandering through text. 

What drives the direction isn't quite the same, an exploration algorythm driven by a heuristic is pushed towards a destination, an LLM is pulled towards its end state like gravity (attractors). So the goal selection and what drives the heuristics aren't the same, but the exploration and selections mechanisms line up well.

## Exploration
Let's start with exploration algorithms. The processes and loops that handle exploring a given domain, A*, Dijkstra's algorithm, chess move calculation and graph search, with some parallel concepts in LLM space.

* Starting point: Where are we? Current location, current board state, current node, llm context.
* Valid moves: What can we do? Which adjacent squares are open? What moves are valid? What edges can I cross? What are the possible next top-k/top-p tokens?
* Move selection: Which do we choose? What square is closest to my goal? Which move scores highest? Which tokens/words weigh the most?
* Apply the move: Perform the action. Move to the square, move to the node, select a word.
* Loop until you run out of potential options, hit an exit condition, or an over weighted `EOS` token.

So a chess / graph search explores like this:

![Chess diagram](/assets/llms-as-pathfinding-through-text/chess-boards.png)

While from an LLM perspective it explores like this:

![Text Exploration](/assets/llms-as-pathfinding-through-text/text-exploration.png)

There are a couple of conceptual differences around this:

* Exploration algorythms can back-track/unwind their stack and try different routes. LLMs work via append only, so while it might revise a previous move or decision, it won't undo it, so it stays in the context history (and can pollute it).
* Graphs and A* use fixed maps, but LLMs procedurally generate their "terrain" at each step based off previous state and selects from what it can see now. 
* Exploration algorithms select their direction based off a Heuristic. LLMs derive their direction from the weights build up from the context/prompt. Eg, asking for a story about Bobby's pet would get a pet branch, asking for a story about Bobby's opinion would get an opinionated branch.

### Depth Vs Breadth First Search
For graph exploration, we can follow two search patterns. We can run a depth first algorithm, follow the "best guess" path and accept the first result we find. This is cheap, but won't find the best result. Breadth first explores each tier of the domain exhaustively, is extremely expensive but will find the absoloute best result.

Most algorithms use a beam search, a hybrid of BFS/DFS that finds a good solution by prioritising searching towards the target, without being exhaustive. This explores a certain depth and breadth based on the most "valuable" moves and how much compute budget we can afford to spend.

There's no direct parallel in LLMs as they don't support apply/revert as an exploration mechanism. They function by append only, and I think the closest concept we have in LLMs is Reasoning behaviour.
* Low reasoning is shallow evaluation, commit quickly. This has a similar outcome to a depth first seach finding the first, most obvious answer.
* High reasoning is deep evaluation before committing. This has a similar outcome to a broader beam search that spends more time finding a better, more complete answer.

The tradeoff is the same, spend more time/compute in the problem space to produce a better/more accurate answer.

## Move Selection
In an exploration algorithm the heuristic used to weight up the available options and prioritise what moves to apply and in what order/priority.

For LLMs the Heuristic is intrinsic to its training, but driven by the initial prompt. A prompt of `Complete the sentence: Harry Potter and the` sets up a chain with a clear end condition. A vague prompt provides no clear objective - "Tell me something interesting" could produce anything.

At the simplest level, a tensor describes the association between a number of concepts. 

Use real examples - Tie these to Bobby pet story example.
> this token is the subject of that verb
> this token is semantically near that noun
> this token appeared before that token

LLMs use huge multi-dimensional arrays describing the association between multiple concepts at the same time, and these are the tensors for running their calculations.

![3d tensor arrow over 3d vector space](/assets/llms-as-pathfinding-through-text/Components_stress_tensor.svg)

The supplied context and generated response feed into the tensors and this in turn is used to determine the weight of the next token/word, which is then selected based on the temperature calculations and other parameters of LLMs.

Your tensors being loaded after receiving your prompt.
![Phalanx spears](/assets/llms-as-pathfinding-through-text/phalanx.gif)

Notes:
* Tensors in LLMs indicate next sequence probability, based off the cumulative weights of the preceding tokens.
* LLMs don't have a destination, they just assign increasing weight to "end of conversation" token.

Talk about Temperature:
Temperature 0 = greedy best-first, no variation, deterministic (always take the highest-probability edge)
High temperature = random walk with strong biases (take one of the most likely possibilities inside the temperature range)
Sampling = stochastic exploration with a bias toward high-probability regions (sample evenly from a high probability region)

## Routing

Let's carry on the Bobby example - Give me a short story about Bobby and his pet. We'll keeping it vague and say the LLM drives the output selection.

![Bobby had a pet dog/cat/snake/lizard](/assets/llms-as-pathfinding-through-text/bobby-had-a-pet.png)

Right now, all routes/options have equal weight. We could chose any of them any and the output would be sensible.

But if we inject an adjective into this sentence structure, this changes the option weighting, depending on which adjective we used.

![Bobby had a furry/scaly pet (dog/cat)/(snake/lizard)](/assets/llms-as-pathfinding-through-text/bobby-had-a-adjective-pet.png)

This adjective selection affects the pet selection, which drives the story subject selection, each chaining onto the next, based on the past. This looking back to generate forward approach is the mechanism behind Attention, as described in [Attention is all you need](https://research.google/pubs/attention-is-all-you-need/);

## Apertures / Doorways

As LLMs follow their routed path through text, it can use phrases to open or close a sequence - these phrases can either narrow or open future options.

### Closing phrases 
These closing phrases lead up to the end of the Reasoning segment and move into response generation.

* I'll write the response as a summary
* Let me structure my review clearly

And these closing phrases lead up to the end of the response:

* Final Verdict: [summary]
* Recommendation: [action plan]
* What do you find [adjective]? Is it [a], [b] or [c]? (Wait for input)
* Want me to do that for you? (Wait for input)

### Opening Phrases
On the other hand, these phrases can be used to open up or generate new paths, keep the reasoning section alive for longer (broader search), or be dropped into the response section as a revised insight.

* But wait...
* Let me think through the key concepts...
* Let me think about what's strong and weak...
* Let me suggest edits based on the instructions in agents.md...

You can see this behaviour if you look at the reasoning logs and follow the chain of thought. It'll spend some time exploring a concept, play around with some hypotheses and reach an end point for a given "thought". At this point it can either move to the next phase, wrap up the response, or explore something else.

These phrases can be viewed as doorways in pathfinding, you have moved up to a portal, and now something needs to happen to access the world/terrain on the other side. Everything converges on an "aperture" and once there you have to "play" an expanding phrase to open the door and move into other areas, otherwise the sequence stops.

![Mysterious door](/assets/llms-as-pathfinding-through-text/door-placeholder.jpg)

Here we are converging on a dead end and we would expect to generate an EOS token in this situation, or move from the Reasoning to the Response Generating phase of output.

![Path converging on a closed doorway](/assets/llms-as-pathfinding-through-text/Closed%20Door.png)

But wait... (Open the door)

![Doorway opening to new area](/assets/llms-as-pathfinding-through-text/Open%20Door.png)

I mentioned gravity/attractors earlier, and you can image the gravity well sitting on the other side of the door, pulling the LLM towards it, until it narrows down its options. Once it hits the dead end, we play the "But wait.." tokens and can expand into the next phase.

## And more
I've got some more analogies in the pipe, coming soon:
* Poisoning data - Confusing signs and useless compasess.
* Trigger Training - Tricking an LLM down a path.
* Memories in the machine - Text in a labarinth of maths.

### Credits
Header photo [Longleat Maze](https://commons.wikimedia.org/wiki/File:Longleat-maze.jpg) by [Niki Odolphie](https://www.flickr.com/photos/99962592@N00/2849889847/), [CC BY 2.0](https://creativecommons.org/licenses/by/2.0/).