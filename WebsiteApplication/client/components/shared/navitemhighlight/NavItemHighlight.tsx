interface NavItemHighlightProps {
    width: number;
    translateX: number;
}

export function NavItemHighlight({width, translateX }: NavItemHighlightProps) {
    return (
        <div
            className="absolute mt-1.5 h-7 bg-gray-200 dark:bg-neutral-700 z-0 rounded-md transition-all duration-300 ease-in-out"
            style={{
                width: `${width}px`,
                transform: `translateX(${translateX}px)`
            }}
        />
    )
}
