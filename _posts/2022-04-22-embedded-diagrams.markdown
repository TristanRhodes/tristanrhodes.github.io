---
layout: post
author: tristan-rhodes
title: Embedded Diagrams
excerpt: Can we embed diagrams directly in blog posts?
featured-image: /assets/embedded-diagrams/featured-image.png
---

## Diagrams.net

Diagrams.net is a _great_ tool for diagramming. I find it vastly superior to Miro, exclusively down to its soothing pastel color palette and selection of line end arrows. Aesthetics aside, taking screen shots, clipping them, sorting them, and updating your post, and once it's live spotting a spelling mistake.... only to have to do it all over again... it just leaves my crying into my tea.

There must be an easier way!

## Can we embed diagrams directly in blog posts?

<p align="center"><iframe src="https://giphy.com/embed/Oc4KnIJ3E7ziqN3l6T" width="480" height="269" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/betnetworks-2022-naacp-bet-image-awards-Oc4KnIJ3E7ziqN3l6T">via GIPHY</a></p></p>

## Hmmmm...

<div class="mxgraph" style="max-width:100%;border:1px solid transparent;" data-mxgraph="{&quot;highlight&quot;:&quot;#0000ff&quot;,&quot;nav&quot;:true,&quot;resize&quot;:true,&quot;toolbar&quot;:&quot;zoom lightbox&quot;,&quot;xml&quot;:&quot;&lt;mxfile host=\&quot;app.diagrams.net\&quot; modified=\&quot;2022-04-25T22:19:20.071Z\&quot; agent=\&quot;5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36\&quot; etag=\&quot;i7nf4BjC3M5B2RAeI8wX\&quot; version=\&quot;17.4.6\&quot; type=\&quot;github\&quot;&gt;&lt;diagram id=\&quot;QXLJM_nJW9eunkgCMoW-\&quot; name=\&quot;Page-1\&quot;&gt;7VnLUtswFP2WLrIs40f8yBICpDOlQ2foDNCdYim2imJ5ZDkPvr6SLccPOYEUJ6GlgYXukWRb59x7dS0P7PF8NWEgib5RiMjAMuBqYF8OLMscWtZA/htwXSCe4RRAyDBUgyrgDj8jBRoKzTBEaWMgp5RwnDTBgMYxCngDA4zRZXPYjJLmXRMQIg24CwDR0XsMeVSgvuVV+BeEw6i8s+mOip45KAerlaQRgHRZg+yrgT1mlPKiNV+NEZHklbwU86639G4ejKGYv2bC89fJ4zTNlhi6aQpG95OfxPvsFldZAJKpBauH5euSAUazGCJ5EXNgXywjzNFdAgLZuxSaCyzic6K6Z5iQMSWU5XPtmSP/JE5jXsOLn8BTzugTqvW4+U/06MtTK14gxtGqBqnlThCdI87WYojqHRqK+nVpF+ayEtIssagmolXOA8p5ws2lK35FQ1G8B92mrdGLoPA3ZVLGIxrSGJCrCr1oClCNuaE0UeAvxPlaBQ/IOG2KglaYP9Taj6JtnDnKupRsGaWxVsZW+lOasQDtWKPyIA5YiPiOcSqC5Pp3iskQARwvmvHYuzKWFghjguXa+wwH6CAfDrvc3remdl9ub7lNtzdN78zRPN/vcPyhcyB2bY3d8yTpl1qA/FnQmVECH01nPWUUp0mtq2cUr4vXg+UTQyNxn3xi/A35xHtlPvHfVT4xzTcp86eZPhbP/lDyL42aNtKsxMmtF9V5kXW7b9bV1O8Uy+S7CTyrldPaIVW4kZpVaXfOGFjXhiVyQLrjPnb3fa5fOd43Wp5TPEDlRxtK3uBaeo32TouI3t2xt2QxOojbHtvbykL1oO7maXt3rxs3MkVV5HVt3CPXs0FPNZHtt5i29Z27+1XgQPuD38GqS7jkIQGxaIeynVdJBSzuUu/5B4on0zpx9TT6r4Hpn7qCHe6TXox3+srlNVntOGjoSi6HI9U5MKlH8dUWqaNTk7rXWdn+pB5nI/S21NEnY3Wv8mJ/Vk97Atlm++Q+rJcdY5KlHDGNdLFC3mS2yVRMY9SiW0GA4DAWJkEzeQXJFg4AOVfwHEOYv5J0CdmUugcFzPaL40g/CnM7JLAPJoFeddxQAAUiiANx8IGkGOpSjI4pRfk8NSnuEFt8HAU6YuG4ApiaAFtO3f9RAU4eAZYeAVkiA8D4gYIolkyJ9iUGIQPzT5osaQQSlHPNt3FYk2wKgqcw5/Q24wRLeXIcAvZ0K2Zhnh8AnRlyv06LgyjT6tjTZzMr6Cw/oTt1nb4+o7S06nhb3Zwi1MXy9xdLmNV34OLcpvqabl/9Bg==&lt;/diagram&gt;&lt;/mxfile&gt;&quot;}"></div>
<script type="text/javascript" src="https://viewer.diagrams.net/js/viewer-static.min.js"></script>

## Yes we can!

With diagrams.net's handy viewer tool, we can host our diagram and export an IFrame wrapper around the viewer tool to get a nifty embedded diagram.

Dump the diagrams somewhere publicly accessible, I'm using a [Diagrams repo](https://github.com/TristanRhodes/Diagrams)

Open your diagram and go to File > Embed > HTML (or an IFrame, but I found you have more control with the HTML side).

// Image

And dump the link into your HTML / markdown.

<p align="center"><iframe src="https://giphy.com/embed/Ri7d8I18cto2jufOKc" width="480" height="270" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/morphin-marvel-deadpool-superhero-Ri7d8I18cto2jufOKc">via GIPHY</a></p></p>

### Credit

Image by <a href="https://pixabay.com/users/clker-free-vector-images-3736/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=311347">Clker-Free-Vector-Images</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=311347">Pixabay</a>