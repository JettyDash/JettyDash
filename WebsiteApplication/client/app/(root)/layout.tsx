import Navbar from "@/components/shared/navbar/Navbar";
import React from "react";

const Layout = ({ children }: { children: React.ReactNode }) => {
    return (
        <main className="relative">
            <Navbar />
            <div>
                <div className="flex flex-1 flex-col px-6 pb-6 pt-10 max-md:pb-14 sm:px-14">
                    <section className="mx-auto w-full">{children}</section>
                </div>
            </div>
        </main>
    );
};

export default Layout;
