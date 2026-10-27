---
layout: post
author: tristan-rhodes
title: LLMs as Pathfinding Through Text
excerpt: Ok... hear me out.
featured-image: /assets/llms-as-pathfinding-through-text/featured-image.jpg
---

In my [previous post](blogging-in-an-ai-world.html), I mentioned my first program was a Chess game. I've also got a long standing passion for [Pathfinding projects](https://github.com/pragmatic-systems/Pragmatic.Playground.Pathfinding) and I've covered [NLP / Text processing and Tokenization in depth](https://github.com/pragmatic-systems/Pragmatic.Playground.Parsing) for over half a decade.

This set me up with two concepts, firstly exploring a domain and prioritising available moves/actions, secondly tokenising words/phrases into concepts based on local context.

These two concepts underpin a lot of LLM behaviour and I'm going to use the exloration mechanism used in pathfinding and domain discovery as a lens for viewing LLM behaviour. Goal selection and what drives the heuristics aren't the same, and I'll try to cover that as we go.

## Exploration
Let's start with exploration algorithms. The processes and loops that handle exploring a given domain, A*, Dijkstra's algorithm, chess move calculation, graph search.

![Chess diagram](/assets/llms-as-pathfinding-through-text/chess-boards.png)

* Starting point: Where are we? Current location, current board state, current node, llm context.
* Valid moves: What can we do? Which adjacent squares are open? What moves are valid? What edges can I cross? What are the possible next words?
* Move selection: Which do we choose? What square is closest to my goal? Which move scores highest? Which tokens/words weigh the most?
* Apply the move: Perform the action. Move to the square, move to the node, select a word.
* Loop until you run out of potential options, hit an exit condition, or an over weighted `EOS` token.

Notes:
Exploration algorithms can back-track, LLMs are append based and can't unwind, but they can re-assess and re-weight things to explore different avenues.
The "Direction" selection for LLMs are derived from the weights built up from the context/prompt.
We're not exploring a fixed "map", more of a procedurally generated environment.

## Selection
How we do selection in Exploration algos vs Token Prediction in LLMs.

In an exploration algorithm the heuristic used to weight up the available options and prioritise what moves to apply and in what order.

[Diagram - Selecting best moves]

For LLMs the Heuristic is intrinsic to its training, but driven by the initial prompt. A prompt of `Complete the sentence: Harry Potter and the` sets up a chain with a clear end condition. A vague prompt provides no clear objective - "Tell me something interesting" could produce anything.

[Diagram - Selecting next word]

At the simplest level, a tensor describes the association between two or more concepts. LLMs use huge multi-dimensional arrays describing the association between multiple concepts at the same time, and these are the tensors for running their calculations.

![3d tensor arrow over 3d vector space](/assets/llms-as-pathfinding-through-text/Components_stress_tensor.svg)

The supplied context and generated response feed into the tensors and this in turn is used to determine the weight of the next token/word, which is then selected based on the temperature calculations and other parameters of LLMs.

Use real examples - Tie these to Bobby pet story example.
> this token is the subject of that verb
> this token is semantically near that noun
> this token appeared before that token

Your tensors being loaded after receiving your prompt.
![Phalanx spears](/assets/llms-as-pathfinding-through-text/phalanx.gif)

Notes:
* LLMs are similar to a higher-order Markov process based off the entire state of the full context window.
* Tensors in LLMs indicate next sequence probability, based off the cumulative weights of the preceding tokens.
* LLMs don't have a destination, they just assign increasing weight to "end of conversation" token.

Talk about Temperature:
Temperature 0 = greedy best-first, no variation, deterministic (always take the highest-probability edge)
High temperature = random walk with strong biases (take one of the most likely possibilities inside the temperature range)
Sampling = stochastic exploration with a bias toward high-probability regions (sample evenly from a high probability region)

## Depth Vs Breadth First Search
For graph exploration, we can follow two patterns. We can run a depth first algorithm, follow the "best guess" path and accept the first result we find. This is cheap, but won't find the most efficient result. Breadth first explores each tier of the domain exhaustively, is extremely expensive but will find the best result.

Most algorithms use a beam search, which is a middle ground that evaluates a prioritised selection of options. This explores a certain depth and breadth based on the most "valuable" moves and how much compute budget we can afford to spend.

There's no direct parallel in LLMs as they don't support apply/revert as an exploration mechanism. They function by append only.

To get around the limitations of this, I feel the most similar concept we have in LLMs is Reasoning behaviour. 
This looks like a pre-weighting activity that primes the tensors ready to generate the body of the response.

Reasoning levels indicate how much time the LLM will spend introspecting its chain of thought. Low would be more akin to a quick depth first search without much fan out, a pass to get a solution fast. Higher reasoning covers more concepts and digs further into associated links, runs a wider selection beam at each step and in turn this takes longer and is more expensive / exhaustive.

Low reasoning = shallow evaluation, commit quickly (greedy or shallow minimax)
High reasoning = deep evaluation before committing (deep minimax / iterative deepening)

## Routing

Let's take an example. Give me a short story about Bobby and his pet. We're keeping it vague and letting the LLM drive the output selection.

Bobby had a big/small/scaly/furry pet dog/cat/rabbit/snake - Show pathing examples.

Depending on different adjectives a story route would run differently for big or scaly, eg: bobby had a _big_ dog or bobby had a _scaly_ snake, then selects what comes next based off the adjectives. The story direction involving the pet will be impacted by the adjective.

[Diagram with highlights]

Bot Attribution:
The bot repeatedly raised Attention as a concept and while it doesn't come from me, it definitely fits.

Normal exploration algorithms look at adjacency and rate the move based on the cost of transition and value of the target state. LLMs can see the entire previous path (the context text) and the attention mechanism enables it to selectively focus on different areas of that text body.

## Apertures / Doorways

As LLMs follow their path through text, it can use phrases to open or close a sequence - these phrases can either narrow or open future options.

Closing phrases (reasoning => response generation):
* I'll write the response as a summary
* Let me structure my review clearly

Closing phrases (end of response):
* Final Verdict: [summary]
* Recommendation: [action plan]

Opening phrases:
* But wait...
* Let me think deeply...

You can see this behaviour if you look at the reasoning logs and follow the chain. It'll spend some time exploring a concept, play around with some hypotheses and reach an end point for a given "thought". At this point it can either move to the next phase, wrap up, or explore something else.

These phrases can be viewed as doorways in pathfinding. Everything converges on an "aperture" and once there you have to "play" an expanding phrase to open the door and expand into other areas, otherwise the sequence stops.

![Mysterious door](/assets/llms-as-pathfinding-through-text/door-placeholder.jpg)

Here we are converging on a dead end and we would expect to generate an EOS token in this situation, or move from the Reasoning to the Response Generating phase of output.

![Path converging on a closed doorway](/assets/llms-as-pathfinding-through-text/Closed%20Door.png)

But wait... (Open the door)

![Doorway opening to new area](/assets/llms-as-pathfinding-through-text/Open%20Door.png)

## Ambiguity
I don't run a super computer, so I'm limited to a 4 quant integer model for local development, that means that my numerical resolution is 0-15 with integer level precision. A full 16 bit floating point model has a resolution of ~0.000061 to 65,504 with the full range of floating point precision.

The impact is reduced fidelity. It's like looking at a blurry picture, harder to tell what it is, easier to get mixed up.

This feeds into a Venn diagram effect where a highly quantised model with well defined concepts will provide distinct answers, but a quantised model blurs the boundary and puts previously separate concepts together into the same bucket.

If you have concepts that are similar, at a lower quant level they merge and become indistinguishable. [They're the same picture meme]

## Terrain Manipulation - Signs and Landmines

### Poisoning Data
* Seed source data with nonsense.
* Floods training data with contradictory / shuffled / incoherent samples.
* Quality drops, output becomes incoherent.
* In pathfinding the algorithm would be lost. The signpost points in random directions, with the relevant ones being drowned out / obscured.
* Link to anthropic poison paper. https://www.anthropic.com/research/small-samples-poison

### Trigger Training
* Train a model to respond to a specific trigger.
* Behaves normally most of the time.
* Executes malicious commands on a trigger.
* Note - Trigger can be driven by rag or from external sources like malicious files on websites.
* In pathfinding the algorithm would be tricked. A certain path would be over incentivised when the explorer gets near it and it charges off like a distracted squirrel.

### Memories in the machine
* Models are not stored as absolute text. It's all Word tokens and association weights.
* You can't search for the text of harry potter in the LLM and see it.
* But if you ask it to complete a sentence starting with the phrase, the weighting kicks in and it completes the sentence.
* In pathfinding this would be like finding a narrow corridor and following it to its conclusion. Similar to trigger training above but not forced.

### Bot Comments (To remove)
The "terrain" (probability distribution over next tokens) is generated by context, not fixed.
At each step, the model samples (or greedily selects) from this distribution.
Some tokens are attractors (high probability peaks) – these are the "corridors."
Some phrases reshape the landscape (apertures open/flatten; conclusions sharpen/peak).
Poisoning corrupts the terrain (creates conflicting attractors).
Triggers create artificial attractors (over-weighted paths).
The model has no global goal – it's a local, context-dependent random walk with strong biases.


### Credits
Header photo [Longleat Maze](https://commons.wikimedia.org/wiki/File:Longleat-maze.jpg) by [Niki Odolphie](https://www.flickr.com/photos/99962592@N00/2849889847/), [CC BY 2.0](https://creativecommons.org/licenses/by/2.0/).