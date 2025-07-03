# REST API Client Code Generator - Documentation Website

This directory contains the static documentation website for the REST API Client Code Generator project.

## Overview

The website is built using Jekyll and deployed to GitHub Pages. It provides comprehensive documentation for the tool including:

- **Homepage** - Overview and quick start guide
- **Features** - Detailed feature documentation
- **Download** - Installation instructions for all platforms
- **CLI Tool** - Command-line tool documentation

## Design Features

- **Clean, minimal design** - Inspired by modern documentation sites
- **Dark/Light mode** - Automatic system preference detection with manual toggle
- **Mobile responsive** - Optimized for all screen sizes
- **Fast loading** - Minimal JavaScript, optimized assets
- **Accessible** - Semantic HTML, proper ARIA labels, keyboard navigation

## Local Development

### Prerequisites

- Ruby 3.x
- Bundler gem

### Setup

1. Navigate to the website directory:
   ```bash
   cd docs/website
   ```

2. Install dependencies:
   ```bash
   bundle install
   ```

3. Serve the site locally:
   ```bash
   bundle exec jekyll serve
   ```

4. Open http://localhost:4000/apiclientcodegen in your browser

### Building for Production

```bash
bundle exec jekyll build
```

## Deployment

The site is automatically deployed to GitHub Pages when changes are pushed to the master branch. The deployment is handled by the GitHub Actions workflow in `.github/workflows/github-pages.yml`.

## File Structure

```
docs/website/
├── _config.yml          # Jekyll configuration
├── _layouts/            # Page templates
│   ├── default.html     # Base layout with header/footer
│   └── page.html        # Page layout for content pages
├── _includes/           # Reusable components
│   ├── header.html      # Site header with navigation
│   └── footer.html      # Site footer
├── _sass/               # SCSS stylesheets
│   └── main.scss        # Main stylesheet with theming
├── assets/              # Static assets
│   ├── css/
│   │   └── main.scss    # SCSS entry point
│   └── js/
│       └── main.js      # JavaScript for dark mode and navigation
├── index.html           # Homepage
├── features.md          # Features page
├── download.md          # Download page
├── cli.md              # CLI documentation
└── Gemfile             # Ruby dependencies
```

## Customization

### Colors and Theming

Colors are defined using CSS custom properties in `_sass/main.scss`. The theme automatically adapts to system preferences and includes a manual toggle.

### Content Updates

- Update `_config.yml` for site-wide settings
- Modify markdown files for content updates
- Edit `_includes/header.html` for navigation changes
- Update `_includes/footer.html` for footer links

### Styling

The site uses a custom CSS framework built with:
- CSS Grid and Flexbox for layouts
- CSS custom properties for theming
- Mobile-first responsive design
- Smooth transitions and animations

## Performance

The site is optimized for performance:
- Minimal JavaScript (~3KB)
- CSS is compressed and minified
- Images are optimized
- No external dependencies except for badges

## Browser Support

- Modern browsers (Chrome, Firefox, Safari, Edge)
- Mobile browsers (iOS Safari, Chrome Mobile)
- Graceful degradation for older browsers