import Image from "next/image";
import Link from "next/link";
import React from "react";
import Theme from "./Theme";
import {SubNavigationMenu} from "@/components/shared/subnavigationmenu/SubNavigationMenu";
import {Divider} from "@nextui-org/divider";
import {Tooltip} from "@nextui-org/react";

const Navbar = () => {
    return (
        <nav
            className="flex-between border-b-2 border-solid dark:border-dark-300 background-light900_dark200 fixed z-50 h-10 w-full shadow-light-300 dark:shadow-none pl-12">
            <div>
                <Link href="/" className="flex items-center gap-1 ">
                    <Image
                        src="/assets/images/site-logo.svg"
                        width={23}
                        height={23}
                        alt="JettyDash"
                    />
                    <p className="h2-bold font-ubuntu text-dark-100 dark:text-light-900 max-sm:hidden">
                        Jetty
                        <span className="text-[#009eff]">Dash</span>
                    </p>
                </Link>
            </div>

            <div className="max-lg:hidden">
                <SubNavigationMenu/>
            </div>

            <div className="flex flex-row flex-none gap-5 items-center ">

                <div>
                    <Theme/>
                </div>
                <Divider orientation="vertical" className="h-6 dark-300"/>

                <Tooltip className="mt-3 h-15 w-32 bg-transparent text-wrap" placement="bottom-end" showArrow={false} content="All systems up and running 👌🏻">
                    <div>
                    <span className="relative flex h-3 w-3 justify-center">
                      <span className="animate-ping absolute inline-flex h-3 w-3 rounded-full bg-green-400 opacity-75"></span>
                      <span className="relative self-center inline-flex rounded-full h-2 w-2 bg-green-500"></span>
                    </span>
                    </div>
                </Tooltip>
                <Divider orientation="vertical" className="h-0"/>


            </div>


        </nav>
    );
};

export default Navbar;
