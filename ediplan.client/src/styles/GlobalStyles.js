import { createGlobalStyle } from 'styled-components';

const GlobalStyles = createGlobalStyle`
/* Reset some default styles */
*,
*::before,
*::after {
  box-sizing: border-box;
  padding: 0;
  margin: 0;

  /* Creating animations for dark mode */
  transition: background-color 0.3s, border 0.3s;
}

/* Sets base font size to 10px; */
html {
  font-size: 62.5%;
}

body {
  font-family: "Poppins", sans-serif;
  color: var(--color-grey-700);

  transition: color 0.3s, background-color 0.3s;
  min-height: 100vh;
  line-height: 1.5;
  font-size: 1.6rem;
}

/* Inherit some properties for form elements */
input,
button,
textarea,
select {
  font: inherit;
  color: inherit;
}

button {
  cursor: pointer;
}

select:disabled,
input:disabled {
  background-color: var(--color-grey-200);
  color: var(--color-grey-500);
}

input:focus,
button:focus,
textarea:focus,
select:focus {
  outline: 3px solid var(--color-brand-200);
  outline-offset: -1px;
}

/* sets line-height to 0, removing extra space for button with SVG */
button:has(svg) {
  line-height: 0;
}

/* Link style */
a {
  color: inherit;
  text-decoration: none;
}

/* Remove default list styles */
ul {
  list-style: none;
}

/* Set overflow behavior for long words and enable
hyphenation for better text readability. */
/* p,
h1,
h2,
h3,
h4,
h5,
h6 {
  overflow-wrap: break-word;
  hyphens: auto;
} */

img {
  max-width: 100%;

  /* For dark mode */
  filter: grayscale(var(--image-grayscale)) opacity(var(--image-opacity));
}


.react-datepicker-wrapper{

  & svg {
          color: var(--color-brand-800);
          height: 2rem;
          width: auto;
        }
}

.react-datepicker__month-container {
  background-color: var(--color-brand-100);
}

.react-datepicker__day {
  background-color: var(--color-brand-50);
  border-radius: var(--border-radius-md);
}

/* CSS vars */
:root {
  /* Violet */
  --color-brand-50: #f5f3ff;
  --color-brand-100: #ede9fe;
  --color-brand-200: #ddd6fe;
  --color-brand-300: #c4b5fd;
  --color-brand-400: #a78bfa;
  --color-brand-500: #8b5cf6;
  --color-brand-600: #7c3aed;
  --color-brand-700: #6d28d9;
  --color-brand-800: #5b21b6;
  --color-brand-900: #4c1d95;
  --color-brand-950: #2e1065;
  /* Grey */
  --color-grey-0: #fff;
  --color-grey-50: #f9fafb;
  --color-grey-100: #f3f4f6;
  --color-grey-200: #e5e7eb;
  --color-grey-300: #d1d5db;
  --color-grey-400: #9ca3af;
  --color-grey-500: #6b7280;
  --color-grey-600: #4b5563;
  --color-grey-700: #374151;
  --color-grey-800: #1f2937;
  --color-grey-900: #111827;
  
  --backdrop-color: rgba(255, 255, 255, 0.1);

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.04);
  --shadow-md: 0px 0.6rem 2.4rem rgba(0, 0, 0, 0.06);
  --shadow-lg: 0 2.4rem 3.2rem rgba(0, 0, 0, 0.12);
  --shadow-tab-active: 8px -0.8rem 1.2rem 0rem rgba(0,0,0,0.06);
  --shadow-tab-inactive: 0px -0.5rem 0.8rem -0.4rem rgba(0,0,0,0.1);

  
  --border-radius-xs: 3px;
  --border-radius-sm: 5px;
  --border-radius-md: 7px;
  --border-radius-lg: 9px;

  --table-font-size: 1.4rem;
  --table-font-weight: 600rem;
  
  /* For dark mode */
  --image-grayscale: 0;
  --image-opacity: 100%;
}

`;

export default GlobalStyles;
