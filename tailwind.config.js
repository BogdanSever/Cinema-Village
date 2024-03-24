/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml',
        './FrontEnd/JS/**/*.js',
        './FrontEnd/CSS/**/*.css',
    ],
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            fontFamily: {
                BarlowSemiCondensed: ['Barlow Semi Condensed', 'sans-serif'],
                Fraunces: ['Fraunces', 'serif']
            }
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
