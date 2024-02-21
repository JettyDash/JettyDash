'use client'
import { useState } from 'react'

import { NavItemHighlight } from '../navitemhighlight/NavItemHighlight'

export function SubNavigationMenu () {

    const [navItemHighlightPropsValues, setNavItemHighlightsPropsValues] = useState<{
        width: number;
        translateX: number;
    } | null>(null)

    // handle mouse events
    function handleHoverNavItem (index: number) {
        switch (index) {
            case 0:
                setNavItemHighlightsPropsValues({
                    width: 85.14,
                    translateX: 0
                })
                break;
            case 1:
                setNavItemHighlightsPropsValues({
                    width: 101.73,
                    translateX: 85.14
                })
                break;
            case 2:
                setNavItemHighlightsPropsValues({
                    width: 85.97,
                    translateX: 186.87
                })
                break;
            case 3:
                setNavItemHighlightsPropsValues({
                    width: 103.54,
                    translateX: 272.84
                })
                break;
            case 4:
                setNavItemHighlightsPropsValues({
                    width: 106.08,
                    translateX: 376.38
                })
                break;
            case 5:
                setNavItemHighlightsPropsValues({
                    width: 75.92,
                    translateX: 482.46
                })
                break;
            default:
                setNavItemHighlightsPropsValues(null)
        }
    }

    function handleLeaveNavItem () {
        setNavItemHighlightsPropsValues(null)
    }


    return (
        <div className="sticky top-0 flex z-10">
            <nav onMouseLeave={handleLeaveNavItem} className={`flex flex-row cursor-pointer `}>
                { navItemHighlightPropsValues != null && (
                    <NavItemHighlight
                        width={navItemHighlightPropsValues.width}
                        translateX={navItemHighlightPropsValues.translateX}
                    />
                )}

                <a onMouseOver={() => handleHoverNavItem(0)} href="/overview" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    Overview
                </a>
                <a onMouseOver={() => handleHoverNavItem(1)} href="/dashboards" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    Dashboards
                </a>
                <a onMouseOver={() => handleHoverNavItem(2)} href="/visualizer" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    Visualizer
                </a>
                <a onMouseOver={() => handleHoverNavItem(3)} href="/datamodels" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    DataModels
                </a>
                <a onMouseOver={() => handleHoverNavItem(4)} href="/connections" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    Connections
                </a>
                <a onMouseOver={() => handleHoverNavItem(5)} href="/settings" className="border-b-2 border-transparent hover:border-gray-400 transitiond-all duration-300 ease-in-out relative px-2 py-2 font-ubuntu text-dark-400 hover:text-dark-200 dark:text-stone-400 dark:hover:text-stone-100">
                    Settings
                </a>
            </nav>
        </div>
    )
}
