// import Link from "next/link";
//
// import type { Metadata } from "next";
//
// export const metadata: Metadata = {
//   title: "DataModels | JettyDash",
// };
//
// export default async function DataModels({ }) {
//   return (
//     <div>
//       <h1>DataModels</h1>
//     </div>
//   );
// }

'use client';
import React from "react";
import {DatabaseCards} from "@/components/connections/databasecards";


export default function DataModels() {
    return (
        <DatabaseCards imageSizeClasses={"h-10 w-10"} baseCardClasses={"size-24"} />
    );
}
