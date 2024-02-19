"use client";

import React, { createContext, useState, useContext, useEffect } from "react";

interface ThemeContextProps {
    mode: string;
    setMode: (mode: string) => void;
}
const ThemeContext = createContext<ThemeContextProps | undefined>(undefined);

export function ThemeProvider({ children }: { children: React.ReactNode }) {
    const [mode, setMode] = useState(() => {
        const savedMode = localStorage.getItem("theme");
        return savedMode || (window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");
    });

    const handleThemeChange = (newMode: string) => {
        setMode(newMode);
        if (newMode === "dark") {
            document.documentElement.classList.add("dark");
            localStorage.setItem("theme", "dark");
        } else {
            document.documentElement.classList.remove("dark");
            localStorage.setItem("theme", "light");
        }
    };

    useEffect(() => {
        handleThemeChange(mode);
    }, [mode]);

    return (
        <ThemeContext.Provider value={{ mode, setMode: handleThemeChange }}>
            {children}
        </ThemeContext.Provider>
    );
}

export function useTheme() {
    const context = useContext(ThemeContext);

    if (context === undefined) {
        throw new Error("useTheme must be used within a ThemeProvider");
    }

    return context;
}
