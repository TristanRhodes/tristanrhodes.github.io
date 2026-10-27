---
layout: post
author: tristan-rhodes
title: LLMs as Pathfinding Through Text
excerpt: Ok... hear me out.
featured-image: /assets/llms-as-pathfinding-through-text/featured-image.jpg
---

In my previous post [here], I mentioned I had a Chess background. I've also got a long standing passion for Pathfinding (link) and I've covered NLP / Text processing and Tokenization in depth (3x links) for over half a decade.

This set me up with two concepts, firstly exploring a domain and prioritising available moves/actions, secondly tokenizing words/phrases into concepts based on local context.

These two concepts underpin a lot of LLM behaviour and I'm going to try and lay them on top of each other conceptually, and frame things as an exploration algortrthm.

## Exploration
Let's start with exploration algorythms. The processes and loops that handle exploring a given domain, A*, Dijkstra's algorythm, chess move calculation, graph search.

* Starting point: Where are we? Current location, current board state, current node, llm context.
* Valid moves: What can we do? Which adjacent squares are open? What moves are valid? What edges can I cross? What are the possible next words?
* Move selection: Which do we chose? What square is closest to my goal? Which move scores highest? Which tokens/words weigh the most?
* Apply the move: Perform the action. Move to the square, move to the node, select a word.
* Loop until you run out of potential options, hit an exit condition, or an over weighted `EOS` token.

Notes:
Exploration algorythms can back-track, LLM's are append based and can't unwind, but they can re-assess and re-weight things to explore different avenues.
The "Direction" selection for LLMs are derived from the weights built up from the context/prompt.
We're not exploring a fixed "map", more of a procedurally generated environment.

## Selection
How we do selection in Exploration algos vs Token Prediction in LLMs.

In an exploration algorythm the heuristic used to weight up the available options and prioritise what moves to apply and in what order.

For LLMs the Heuristic is driven by the initial prompt. A prompt of `Complete the sentence: Harry Potter and the` sets up a chain with a clear end condition. A vague prompt provides no clear objective - "Tell me something interesting" could produce anything.

At the simplest level, a tensor describes the association between two or more concepts. LLMs use huge multi-dimensional arrays describing the association between multiple concepts at the same time, and these are the tensors for running their calculations.

The supplied context and generated response feed into the tensors and this in turn is used to determine the weight of the next token/word, which is then selected based on the temperature calculations and other parameters of LLMs.

Bot example: "this token is the subject of that verb" and "this token is semantically near that noun" and "this token appeared before that token."

Notes:
* LLMs are similar to a Markov process based off the entire state of the full context window.
* Tensors in LLMs indicate next sequence probability, based off the cumulative weights of the preceding tokens.
* LLMs don't have a destination, they just assign increasing weight to "end of converstaion" token.

Talk about Temperature:
Temperature 0 = greedy best-first (always take the highest-probability edge)
High temperature = random walk with strong biases
Sampling = stochastic exploration with a bias toward high-probability regions

## Depth Vs Breadth First Search
For graph exploration, we can follow two patterns. We can run a depth first algorythm, follow the "best guess" path and accept the first result we find. This is cheap, but won't find the most efficient result. Breadth first explores each tier of the domain exhaustively, is extreemly expensive but will find the best result.

Most algorythms use a beam search, which is a middle ground which evaluates a prioritised selection of options. This explores a certain depth and breadth based on the most "valuable" moves and how much compute budget we can afford to spend.

There's no direct parrable in LLMs as they don't support apply/revert as an exploration mechanism. They function by append only.

To get around the limitations of this, I feel the most similar concept we have in LLMs is Reasoning behaviour. This looks like a pre-weighting activity that primes the tensors in the main section for the body of the response.

Reasoning levels indicate how much time the LLM will spend introspecting its chain of thought. Low would be more akin to a narrow beam search, a quick pass to get a solution fast. Higher reasoning covers more concepts and digs further into associated links, runs a wider selection beam at each step and in turn this takes longer and is more expensive / exhaustive.

Low reasoning = shallow evaluation, commit quickly (greedy or shallow minimax)
High reasoning = deep evaluation before committing (deep minimax / iterative deepening)

## Ambiguity
I don't run a super computer, so I'm limited to a 4 quant integer model for local development, that means that my numerical resolution is 0-15 with integer level precision. A full 16 bit floating point model has a resolution of ~0.000061 to 65,504 with the full range of floating point precision.

The impact is reduced fidelity. It's like looking at a blurry picture, harder to tell what it is, easier to get mixed up.

This feeds into a venn diagram effect where a highly quantised model with well defined concepts will provide distinct answers, but a quantitised model blurrs the boundary and puts previously seperate concepts together into the same bucket.

If you have concepts that are similar, at a lower quant level they merge and become indistinguishable. [They're the same picture meme]

## Routing

Let's take an example. Give me a short story about Bobby and his pet. We're keeping it vague and letting the LLM drive the output selection.

Bobby had a big/small/scaly/furry pet dog/cat/rabbit/snake - Show pathing examples.

Depending on different adjectives a story route would run differently for big or scaly, eg: bobby had a _big_ dog or bobby had a _scaly_ snake, then selects what comes next based off the adjectives. The story direction involving the pet will impacted by the adjective.

[Diagram with highlights]

Bot Attribution:
The bot repeatedly raised Attention as a concept and while I can't say it is insight that comes from me, it is something that is relevant and fits the model.

Normal exploration algorythms look at adjacency and rate the move based on the cost of transition and value of the target state. LLMs can see the entire previous path (the context text) and the attention mechanism enables it to selectively focus on different areas of that text body it's like it has complete map visibility when making a move decision.

## Notes

### Apertures / Doorways
* LLMS have closing phrases that narrow options and opening phrases that expand possible options. "But wait.." or "Let me think deeply..." set up another sequence chain, rather than closing the conversation.
* These can be viewed as doorways in pathfinding. Everything converges on an "aperture" and once there you have to "play" an expanding phrase to open the door and expand into other areas.

### Poisoning Data
* Seed source data with nonsense.
* Floods training data with contradictory / shuffled / incoherant samples.
* Quality drops, output becomes incoherant.
* In pathfinding the algorythm would be lost. The signpost points in random directions, with the relevant ones being drowned out / obscured.
* Link to anthropic poison paper. https://www.anthropic.com/research/small-samples-poison

### Trigger Training
* Train a model to respond to a specific trigger.
* Behaves normally most of the time.
* Executes malicious commands on a trigger.
* Note - Trigger can be driven by rag or from external sources like malicious files on websites.
* In pathfinding the algotryhm would be tricked. A certain path would be over incentivised when the explorer gets near it and it charges off like a distracted squirrel.

### Memories in the machine
* Models are not stored as absoloute text. It's all Word tokens and association weights.
* You can't search for the text of harry potter in the LLM and see it.
* But if you ask it to complete a sentence starting with the phrase, the weighting kicks in and it completes the sentence.
* In pathfinding this would be like finding a narrow corridor and following it to it's conclusion. Similar to trigger training above but not forced.

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