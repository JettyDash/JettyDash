import Image from "next/image";
import Link from "next/link";
import React from "react";
import Theme from "./Theme";

const Navbar = () => {
  return (
    <nav className="flex-between background-light900_dark200 fixed z-50 w-full gap-5 p-1 shadow-light-300 dark:shadow-none sm:px-12">
      <Link href="/" className="flex items-center gap-1">
        <Image
          src="/assets/images/site-logo.svg"
          width={23}
          height={23}
          alt="JettyDash"
        />
        <p className="h2-bold font-ubuntu text-dark-100 dark:text-light-900 max-sm:hidden">
          Jetty
          <span className="text-primary-500">Dash</span>
        </p>
      </Link>

        Overview Dashboards Visualizer DataModels Connections Settings
        <div className="flex-between gap-5">
        <Theme />
      </div>
    </nav>
  );
};

export default Navbar;
