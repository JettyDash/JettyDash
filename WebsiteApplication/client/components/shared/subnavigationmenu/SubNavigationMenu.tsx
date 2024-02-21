'use client';
import { useState } from 'react';
import { usePathname } from 'next/navigation';
import { NavItemHighlight } from '../navitemhighlight/NavItemHighlight';
import { sidebarLinks } from "@/constants";
import {NavItemHighlightProps, SidebarLink} from "@/types";


export function SubNavigationMenu() {
    const pathname = usePathname();
    const [navItemHighlightPropsValues, setNavItemHighlightsPropsValues] = useState<NavItemHighlightProps | null>(null);

    function handleHoverNavItem(index: number) {
        const { width, translateX } = getNavItemHighlightProps(index);
        setNavItemHighlightsPropsValues({ width, translateX });
    }

    function handleLeaveNavItem() {
        setNavItemHighlightsPropsValues(null);
    }

    function getNavItemHighlightProps(index: number): NavItemHighlightProps {
        const widths = [85.14, 101.73, 85.97, 103.54, 106.08, 75.92];
        const translateXs = [0, 85.14, 186.87, 272.84, 376.38, 482.46];
        return { width: widths[index], translateX: translateXs[index] };
    }

    return (
        <div className="sticky top-0 flex z-10">
            <nav onMouseLeave={handleLeaveNavItem} className="flex flex-row cursor-pointer">
                {navItemHighlightPropsValues && (
                    <NavItemHighlight {...navItemHighlightPropsValues} />
                )}
                {sidebarLinks.map((link: SidebarLink, index: number) => (
                    <a
                        key={index}
                        onMouseOver={() => handleHoverNavItem(index)}
                        href={link.route}
                        // aria-checked={pathname === link.route}
                        data-state={pathname === link.route ? 'active' : ''}
                        className="tab"
                    >
                        {link.label}
                    </a>
                ))}
            </nav>
        </div>
    );
}
