import type {Metadata} from "next";
import {Divider} from "@nextui-org/divider";
import ConnectionTable from "@/components/connections/connectiontable";
import React from "react";


export const metadata: Metadata = {
    title: "JettyDash",
    description:
        "JettyDash is a modern, open-source dashboard creator for your business.",
    icons: {
        icon: "/assets/images/site-logo.svg",
    },
};

export default async function Connections({}) {
    return (
        <div className="flex h-full">
            <div className="flex-1"></div>
            <div className="flex-4 flex-col">
                <div className="flex-between px-20 mt-3 pt-3">
                    <div>
                        <h1 className="h1-bold font-ubuntu text-dark-200 dark:text-stone-300">Current Connections</h1>
                        <p className="body-regular relative inline-flex font-ubuntu text-dark-500 dark:text-stone-400 mt-3 mr-1">Read
                            directly from
                            databases without storing any information.
                        </p>
                        <div className="learnmore relative inline-flex group items-center">
                            <a href="/docs"
                               className="inline-flex relative text-[#3291FF] body-regular text-start font-ubuntu">Learn
                                More</a>
                            <svg className="w-4 h-4 inline-flex ml-1 relative text-[#3291FF]"
                                 xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                 stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                                      d="M10 6H6a2 2 0 00-2 2v10a2 2 0 002 2h10a2 2 0 002-2v-4M14 4h6m0 0v6m0-6L10 14"/>
                            </svg>
                            <div
                                className="svg_underline absolute bottom-0 left-0 pointer-events-none bg-transparent transition-all"></div>
                        </div>

                    </div>


                </div>
                <Divider orientation="horizontal" className="my-5 dark-300"/>

                <div
                    className="border border-solid border-light-300 bg-stone-50 shadow-md rounded-xl px-10 py-5 dark:border-dark-300 dark:bg-stone-900">
                    <ConnectionTable/>
                </div>

            </div>
            <div className="flex-1"></div>
        </div>
    );
}