const flowbiteReact = require("flowbite-react/plugin/tailwindcss");

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './App.{js,ts,tsx}',
    './components/**/*.{js,ts,tsx}',
    ".flowbite-react\\class-list.json"
  ],
  presets: [require('nativewind/preset')],
  theme: {
    extend: {},
  },
  plugins: [flowbiteReact],
};