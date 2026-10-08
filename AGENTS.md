# AGENTS.md

Jekyll blog published to GitHub Pages at https://www.tristan-rhodes.co.uk.

## Running locally

- `docker compose up --build` serves at http://localhost:4000 with live reload; the repo is volume-mounted so edits apply without rebuilding.
- Dockerfile pins Ruby 2.7 (github-pages 220 / jekyll 3.9.0 don't run on Ruby 3) and sets `PAGES_REPO_NWO`, which `jekyll-github-metadata` requires.

## Repo layout

- `_posts/` — posts as `YYYY-MM-DD-slug.markdown` with front matter (`layout`, `author`, `title`, `excerpt`, `featured-image`).
- `assets/<post-slug>/` — per-post images, referenced as absolute paths.
- Top-level `.md` files are standalone pages with a `permalink`.
- `_layouts/` + `_includes/` wrap the `jekyll-theme-slate` theme; `css/`, `js/`, `images/` are static assets.
- `_site/` is build output; gitignored, don't edit.

## Conventions

- New post = `_posts/YYYY-MM-DD-slug.markdown` plus images under `assets/<slug>/`; keep the filename date in sync with the front matter.
- Keep `Gemfile.lock` as-is (pinned to the Ruby 2.x github-pages set).
- `PlannedPosts.md` is an internal note, not published.

## Authoring rules

- Generate scaffolding only: file layout, front matter, asset wiring.
- When creating new posts, leave `title`, `excerpt`, and body as placeholders for the author.
- When reviewing copy, flag issues (clarity, contradictions, technical inaccuracy) but do not suggest rewordings, new content or technical direction.