import {nextui} from "@nextui-org/react";


/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: ["class"],
  lightMode: ["class"],
  content: [
    "./node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}",
    "./pages/**/*.{ts,tsx}",
    "./components/**/*.{ts,tsx}",
    "./app/**/*.{ts,tsx}",
    "./src/**/*.{ts,tsx}",
  ],
  theme: {
    container: {
      center: true,
      padding: "2rem",
      screens: {
        "2xl": "1400px",
      },
    },
    extend: {

      flex: {
        1: "1 1 0%",
        2: "2 2 0%",
        3: "3 3 0%",
        4: "4 4 0%",
        5: "5 5 0%",
        6: "6 6 0%",
        7: "7 7 0%",
        8: "8 8 0%",
        9: "9 9 0%",
      },
      colors: {
        // primary: {
        //   600: "#075FA5",
        //   500: "#009eff",
        //   100: "#FFF1E6",
        // },
        dark: {
          100: "#1C1B1B", // background blank, form padding blank
          200: "#272626", // Padding blank
          300: "#32302F", // shadow color
          400: "#615E5B", // hover box border on
          500: "#7C7673", // form inside text, hover on
          600: "#F0EEEA", // form box upper text
        },
        light: {
          900: "#FEFCF8", // background blank
          850: "#F7F5F1", // Padding blank
          700: "#F0EEEA", // shadow color
          800: "#FBF9F7", // form padding blank
          500: "#7C7673", // form inside text
          400: "#4A4645", // form box upper text
          300: "#DED6D0", // hover box border on
        }
      },
      fontFamily: {
        ubuntu: ["var(--font-ubuntu)"],
      },
      boxShadow: {
        "light-100":
            "0px 12px 20px 0px rgba(184, 184, 184, 0.03), 0px 6px 12px 0px rgba(184, 184, 184, 0.02), 0px 2px 4px 0px rgba(184, 184, 184, 0.03)",
        "light-200": "10px 10px 20px 0px rgba(218, 213, 213, 0.10)",
        "light-300": "-10px 10px 20px 0px rgba(218, 213, 213, 0.10)",
        "dark-100": "0px 2px 10px 0px rgba(46, 52, 56, 0.10)",
        "dark-200": "2px 0px 20px 0px rgba(39, 36, 36, 0.04)",
      },
      backgroundImage: {
        "auth-dark": "url('/assets/images/auth-dark.png')",
        "auth-light": "url('/assets/images/auth-light.png')",
      },
      screens: {
        xs: "420px",
      },
      keyframes: {
        "accordion-down": {
          from: { height: 0 },
          to: { height: "var(--radix-accordion-content-height)" },
        },
        "accordion-up": {
          from: { height: "var(--radix-accordion-content-height)" },
          to: { height: 0 },
        },
      },
      animation: {
        "accordion-down": "accordion-down 0.2s ease-out",
        "accordion-up": "accordion-up 0.2s ease-out",
      },
    },
  },
  plugins: [require("tailwindcss-animate"), require("@tailwindcss/typography"),
    nextui({
      themes: {
        light: {
          colors: {
            background: "#F7F5F1", // or DEFAULT
            // foreground: "#11181C", // or 50 to 900 DEFAULT
            // primary: {
            //   //... 50 to 900
            //   foreground: "#FFFFFF",
            //   DEFAULT: "#006FEE",
            // },
            // // ... rest of the colors
          },
        },
        dark: {
          colors: {
            background: "#1C1B1B", // or DEFAULT
            /*foreground: "#ECEDEE", // or 50 to 900 DEFAULT
            primary: {
              //... 50 to 900
              foreground: "#FFFFFF",
              DEFAULT: "#006FEE",
            },*/
          },
        },
        /*mytheme: {
          // custom theme
          extend: "dark",
          colors: {
            primary: {
              DEFAULT: "#BEF264",
              foreground: "#000000",
            },
            focus: "#BEF264",
          },
        },*/
      },
    })
  ],
};
