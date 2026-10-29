---
layout: post
author: tristan-rhodes
title: LLMs as Pathfinding Through Text
excerpt: Ok... hear me out.
featured-image: /assets/llms-as-pathfinding-through-text/featured-image.jpg
---

I've [mentioned my first program was a Chess game](/2026/10/06/blogging-in-an-ai-world.html). I've also had a long standing passion for exploration and [pathfinding projects](https://github.com/pragmatic-systems/Pragmatic.Playground.Pathfinding) and I've covered [NLP / Text processing and Tokenization in depth](https://github.com/pragmatic-systems/Pragmatic.Playground.Parsing) for over half a decade.

This gives me a solid grounding in two concepts, firstly exploring a domain and prioritising available moves/actions, secondly mapping words/phrases into tokens based on local context. 

These two concepts underpin a lot of LLM behaviour and I'm going to use the exploration mechanism used in pathfinding and domain discovery as a lens for viewing an LLM's meandering through text. 

What drives the direction isn't quite the same, an exploration algorithm driven by a heuristic is pushed towards a destination, an LLM is pulled towards its end state like gravity (attractors). So the goal selection and what drives the heuristics aren't the same, but the exploration and selections mechanisms line up well.

## Exploration
Let's start with exploration algorithms. The processes and loops that handle exploring a given domain, A*, Dijkstra's algorithm, chess move calculation and graph search, with some parallel concepts in LLM space.

* Starting point: Where are we? Current location, current board state, current node, LLM context.
* Valid moves: What can we do? Which adjacent squares are open? What moves are valid? What edges can I cross? What are the possible next top-k/top-p tokens?
* Move selection: Which do we choose? What square is closest to my goal? Which move scores highest? Which tokens/words weigh the most?
* Apply the move: Perform the action. Move to the square, move to the node, select a word.
* Loop until you run out of potential options, hit an exit condition, or generate an `EOS` token.

So a chess / graph search explores like this:

![Chess diagram](/assets/llms-as-pathfinding-through-text/chess-boards.png)

While from an LLM perspective it explores like this:

![Text Exploration](/assets/llms-as-pathfinding-through-text/text-exploration.png)

There are a couple of conceptual differences around this:

* Exploration algorithms can back-track/unwind their stack and try different routes. LLMs work via append only, so while it might revise a previous move or decision, it won't undo it, so it stays in the context history (and can pollute it).
* Graphs and A* use fixed maps, but LLMs procedurally generate their "terrain" at each step based off previous state and selects from what it can see now. 
* Exploration algorithms select their direction from a heuristic. LLMs derive their direction by projecting context/prompt through the weights. Eg, asking for a story about Bobby's pet would get a pet activity branch, asking for a story about Bobby's toy would get a branch about play time.

## Depth Vs Breadth First Search
For graph exploration, we can follow two main search patterns. We can run a depth first algorithm, follow the "best guess" path and accept the first result we find. This is cheap, but won't find the best result. Breadth first explores each tier of the domain exhaustively, is extremely expensive but will find the absolute best result.

A beam search is a middle ground between BFS/DFS that finds a good solution by prioritising searching towards the target, without being exhaustive. This explores a certain depth and breadth based on the most "valuable" moves and how much compute budget we can afford to spend.

This beam search concept shows up in LLMs Reasoning behaviour:
* Low reasoning is shallow evaluation, commit quickly. This has a similar outcome to a thin beam search that behaves like a depth first search, finding the first, most obvious answer.
* High reasoning is deep evaluation before committing. This has a similar outcome to a broader beam search that spends more time finding a better, more complete answer.

The tradeoff is the same, spend more time/compute in the problem space to produce a better/more accurate answer.

## Move Selection

Let's carry on the Bobby example - Give me a short story about Bobby and his pet. We'll keep it vague and say the LLM drives the output selection.

![Bobby had a pet dog/cat/snake/lizard](/assets/llms-as-pathfinding-through-text/bobby-had-a-pet.png)

Right now, all routes/options have (mostly) equal weight. We could choose any of them and the output would be sensible.

But if we inject an adjective into this sentence structure, this changes the option weighting, depending on which adjective we use.

![Bobby had a furry/scaly pet (dog/cat)/(snake/lizard)](/assets/llms-as-pathfinding-through-text/bobby-had-a-adjective-pet.png)

This adjective selection affects the pet selection, which drives the story subject selection, each chaining onto the next, based on the past. This looking back to generate forward approach is the mechanism behind Attention, as described in [Attention is all you need](https://research.google/pubs/attention-is-all-you-need/).

Compared to an exploration algorithm, where the heuristic is used to weight up the available options and prioritise what moves to apply and in what order/priority, for an LLM the Heuristic is intrinsic to its training, but driven by the initial prompt + context generated so far. 

A prompt of `Complete the sentence: Harry Potter and the` sets up a chain with a clear direction. A vague prompt - "Tell me something interesting" provides no clear objective and could go anywhere.

## Tensors

For LLMs this selection process is calculated through Tensors.

At the simplest conceptual level, a tensor encodes the relationship between concepts - how strong a connection between things is and in what context. It's way more [complicated than that](https://en.wikipedia.org/wiki/Tensor_(machine_learning)) - internally there are huge multi-dimensional arrays which describe multiple concept associations at the same time, but we're trying to keep it simple.

A real world example of these kinds of associations can be represented like this:
![Bobby had a pet - annotated](/assets/llms-as-pathfinding-through-text/text-exploration-annotated.png)

Each of the comment boxes in the above diagram describes a relationship concept that would be encoded into the model weights. These weights are the tensor values which feed into the calculations for next word selection, and they end up being represented like this:
![Tensor Matrices](/assets/llms-as-pathfinding-through-text/Tensor_dimensions.svg)

The supplied context and generated response feed into the tensors and this in turn is used to determine the weight of the next token/word, which is then selected based on the temperature settings and other parameters of LLMs.

The temperature setting is the spread of selection over the target, this determines predictability/repeatability and how close the LLM will adhere to a given thread:
* Temperature 0: always take the highest-probability (no variation, deterministic)
* Temperature 1: natural output (variation follows the model variation)
* Temperature >1: sample from outside of normal range (unusual associations become more probable, eg furry snake)

Based off this, LLMs don't have a hard destination encoded in a heuristic, they are guided by the tensors, and move forward one step at a time until the `EOS` is selected. This ties in well to the gravity analogy - pulling the result towards `EOS` like a river following gravity.

## Apertures / Doorways

As LLMs follow their path through text, they can use phrases to open or close a sequence - these phrases can either narrow or open future options.

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

You can see this behaviour if you look at the reasoning logs and follow the chain of thought. It'll spend some time exploring a concept, play around with some hypotheses and reach an end point for a given "thought". At this point it can either explore something else, move to the next phase or wrap up the response.

These phrases can be viewed as doorways in pathfinding. You've moved up to one, and now something needs to happen to access the world/terrain on the other side. Everything converges on an "aperture" and once there you have to "play" an expanding phrase to open the door and move into other areas, otherwise the sequence stops.

![Mysterious door](/assets/llms-as-pathfinding-through-text/door-placeholder.jpg)

Here we are converging on a dead end and we would expect to generate an EOS token in this situation, or move from the Reasoning to the Response Generating phase of output.

![Path converging on a closed doorway](/assets/llms-as-pathfinding-through-text/closed-door.png)

But wait... (Open the door)

![Doorway opening to new area](/assets/llms-as-pathfinding-through-text/open-door.png)

I mentioned gravity/attractors earlier, and you can imagine the gravity well sitting on the other side of the door, pulling the LLM towards it, until it narrows down its options. Once it hits the dead end, we play the "But wait.." tokens, the door opens, the gravity well moves and it can expand into the next phase.

## Wrap up
Ok, I think I've stretched / bent the analogy as much as I can. It's not a perfect fit by any means, but I think the concepts are close enough that it gives a good mental scaffold for understanding how these things work under the hood. 

I've got some more analogies in the pipeline:
* Data Poisoning - Confusing signs and useless compasses.
* Trigger Training - Tricking an LLM down a path.
* Correlations and Hallucinations - Conflating concepts at lower quants.
* Memories in the machine - Finding text in a labyrinth of maths.

Coming soon!

### Credits
Header photo [Longleat Maze](https://commons.wikimedia.org/wiki/File:Longleat-maze.jpg) by Niki Odolphie.