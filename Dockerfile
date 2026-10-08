# Gemfile.lock is pinned to github-pages 220 / jekyll 3.9.0, which only
# supports Ruby 2.x. Ruby 3 will break the build, so stay on 2.7.
FROM ruby:2.7-slim

# ruby:2.7-slim is Debian Bullseye, now EOL. Point apt at the permanent archive instead.
RUN echo 'deb http://archive.debian.org/debian bullseye main' > /etc/apt/sources.list \
    && echo 'deb http://archive.debian.org/debian-security bullseye-security main' >> /etc/apt/sources.list \
    && apt-get -o Acquire::Check-Valid-Until=false update -y \
    && apt-get -o Acquire::Check-Valid-Until=false install -y --no-install-recommends build-essential git \
    && rm -rf /var/lib/apt/lists/*

# jekyll-github-metadata (pulled in by github-pages) is used in
# _layouts/default.html and needs the repo's owner/name. Supplied as Env Var.
ENV PAGES_REPO_NWO=TristanRhodes/tristanrhodes.github.io

WORKDIR /app

# Install gems first for layer caching.
COPY Gemfile Gemfile.lock ./
RUN bundle install --jobs 4 --retry 3

COPY . .

EXPOSE 4000

CMD ["bundle", "exec", "jekyll", "serve", "--host", "0.0.0.0", "--port", "4000", "--watch"]
