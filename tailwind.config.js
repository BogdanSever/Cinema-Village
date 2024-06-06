/** @type {import('tailwindcss').Config} */
module.exports = {
    important: true,
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
                'Bebas-Neue': ["Bebas Neue", 'serif'],
                'Lato': ["Lato", 'serif'],
                'Playfair-Display': ["Playfair Display", 'serif'],
                'Montserrat': ['Montserrat', 'serif'],
                'Nunito': ['Nunito', 'serif'],
                'Lora': ['Lora', 'serif'],
                'GothicA1': ['Gothic A1', 'serif'],
                'Hanken-Grotesk': ['Hanken Grotesk', 'serif'],
            },
            colors: {
                'mustard': {
                    '100': "#FFD447",
                    '200': "#fcbe11",
                },
                'night': "#121113",
                'lavender-bush': "#EEEEEE",
                'zeus': {
                    '50': '#f8f6f5',
                    '100': '#e8e4df',
                    '200': '#d0c9bf',
                    '300': '#b0a798',
                    '400': '#8f8572',
                    '500': '#746b58',
                    '600': '#5c5545',
                    '700': '#4c4639',
                    '800': '#3e3a31',
                    '900': '#36332b',
                    '950': '#23211a',
                },
                gridAutoRows: {
                    '2fr': 'minmax(0, 2fr)',
                },
            },
        },
        variants: {
            extend: {},
        },
        plugins: [],


    }
}
