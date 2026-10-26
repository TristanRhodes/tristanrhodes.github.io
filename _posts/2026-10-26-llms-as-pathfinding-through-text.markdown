---
layout: post
author: tristan-rhodes
title: LLMs as Pathfinding Through Text
excerpt: Ok... hear me out.
featured-image: /assets/llms-as-pathfinding-through-text/featured-image.jpg
---

In my previous post [here], I mentioned I had a Chess background. I've also got a long standing passion for Pathfinding (link) and I've covered NLP / Text processing and Tokenization in depth (3x links) for over half a decade.

This set me up with two concepts, firstly exploring a domain and prioritising available moves/actions, secondly tokenizing words/phrases into concepts based on local context.

These two concepts underpin a lot of LLM behaviour and I'm going to try and lay them on top of each other conceptually.

## Exploration
Let's start with exploration algorythms. The processes and loops that handle exploring a given domain, A*, Dijkstra's algorythm, chess move calculation, graph search.

* Starting point: Where are we? Current location, current board state, current node, llm context.
* Valid moves: What can we do? Which adjacent squares are open? What moves are valid? What edges can I cross? What are the possible next words?
* Move selection: Which do we chose? What square is closest to my goal? Which move scores highest? Which tokens/words weigh the most?
* Apply the move: Perform the action. Move to the square, move to the node, select a word.
* Loop until you run out of potential options, hit an exit condition, or an over weighted `EOS` token.

NOTE: Comment from the bot - for an LLM, all moves are valid, no matter how low probability. It can "Walk through walls." This is apparently where hallucinations come from and why they are unavoidable. Always good when a tool appreciates it's own fallibility.

## Selection
TODO: How we do selection in Exploration algos vs Token Prediction in LLMs.
Heuristics. 

In an exploration algorythm the heuristic used to weight up the available options and prioritise what moves to apply and in what order.

For LLMs the Heuristic is driven by the initial prompt. A prompt of `Complete the sentence: Harry Potter and the` sets up a clear chain with a clear end condition. A vague prompt provides no clear objective - "Tell me something interesting" could produce anything.

## Depth Vs Breadth First
For graph exploration, we can follow two patterns. We can run a depth first algorythm, follow the "best guess" path and accept the first result we find. This is cheap, but won't find the most efficient result. Breadth first explores each tier of the domain exhaustively, is extreemly expensive but will find the best result.

Most algorythms use some weighted varient of these, we explore a certain depth and breadth based on the most "valuable" moves and how much compute budget we can afford to spend.

There's no real parrable in LLMs as they don't support apply/revert as an exploration mechanism. They function by append only.

To get around the limitations of this, I feel the most similar concept we have in LLMs is Reasoning behaviour. This looks like a pre-weighting activity that primes the tensors in the main section for the body of the response.

Reasoning levels indicate how much time the LLM will spend introspecting its chain of thought. Low would be more akin to a depth first search, a simple pass to get a solution quickly. Higher reasoning fans out to cover more concepts and dig into associated links, this takes longer and is more expensive, functioning more like a breadth first search.

## Notes
Main points:
Have a chess and pathfinding background, combined with NLP / Text Processing. (X)
Feel like we can frame LLM's as pathfinding through text. (X)
Tensor Weights from past tokens determine selection weighting for next tokens.
Quantitisation determines how much spread we have between word/concepts. Token concepts can be considered like a Venn Diagram. Lower Quants overlap more and have less distinction, harder to select correct values, everything "looks the same". "It's the same picture" meme.
Bobby had a big/small/scaly/furry pet dog/cat/rabbit/snake - Show pathing examples.
Depending on different adjectives a story route would run differently for big or scaly, eg: bobby had a _big_ dog or bobby had a _scaly_ snake, then selects what comes next based off the adjectives. The story direction involving the pet will impacted by the adjective.

Concept:
A* / Dyiskas Algorythm/Chess Domain Exploration looks at the following:
* Where are we? This is the starting point.
* Where do we need to go? What is the end point.
* Where can I move? What can be done.
* Of these options, which is the best/most relevant? Evaluate the possible options.
* Apply the best one.
* Loop until you run out of potential options.

Differences:
* LLMs are more like a Markov (not a chain) process based off the entire previous state.
* Tensors in LLMs indicate next sequence probability, based off the cumulative weights of the preceding tokens.
* LLMs don't have a destination, they just assign increasing weight to "end of converstaion" token.
* LLMs have a direction derived from the weights in the context/prompt.
* LLMs don't back track or un-wind the search stack.
* We're not exploring a "map", more of a concept space.

Apertures / Doorways:
* LLMS have closing phrases that narrow options and opening phrases that expand possible options. "But wait.." or "Let me think deeply..." set up another sequence chain, rather than closing the conversation.
* These can be viewed as doorways in pathfinding. Everything converges on an "aperture" and once there you have to "play" an expanding phrase to open the door and expand into other areas.

Poisoning Data:
* Seed source data with nonsense.
* Floods training data with contradictory / shuffled / incoherant samples.
* Quality drops, output becomes incoherant.
* In pathfinding the algorythm would be lost. The signpost points in random directions, with the relevant ones being drowned out / obscured.
* Link to anthropic poison paper. https://www.anthropic.com/research/small-samples-poison

Trigger Training:
* Train a model to respond to a specific trigger.
* Behaves normally most of the time.
* Executes malicious commands on a trigger.
* Note - Trigger can be driven by rag or from external sources like malicious files on websites.
* In pathfinding the algotryhm would be tricked. A certain path would be over incentivised when the explorer gets near it and it charges off like a distracted squirrel.

Memories in the machine:
* Models are not stored as absoloute text. It's all Word tokens and association weights.
* You can't search for the text of harry potter in the LLM and see it.
* But if you ask it to complete a sentence starting with the phrase, the weighting kicks in and it completes the sentence.
* In pathfinding this would be like finding a narrow corridor and following it to it's conclusion. Similar to trigger training above but not forced.

Bot Comments:
The "terrain" (probability distribution over next tokens) is generated by context, not fixed.
At each step, the model samples (or greedily selects) from this distribution.
Some tokens are attractors (high probability peaks) – these are the "corridors."
Some phrases reshape the landscape (apertures open/flatten; conclusions sharpen/peak).
Poisoning corrupts the terrain (creates conflicting attractors).
Triggers create artificial attractors (over-weighted paths).
The model has no global goal – it's a local, context-dependent random walk with strong biases.


### Credits
Header photo [Longleat Maze](https://commons.wikimedia.org/wiki/File:Longleat-maze.jpg) by [Niki Odolphie](https://www.flickr.com/photos/99962592@N00/2849889847/), [CC BY 2.0](https://creativecommons.org/licenses/by/2.0/).