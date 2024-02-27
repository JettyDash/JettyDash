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
import {RadioGroup} from "@nextui-org/react";
import {CustomRadio} from "@/app/(root)/datamodels/customRadio";

export default function DataModels() {
    return (
        <RadioGroup orientation="horizontal" label="Plans">
            <CustomRadio description="Up to 20 items" value="free">
                Free
            </CustomRadio>
            <CustomRadio description="Unlimited items. $10 per month." value="pro">
                Pro
            </CustomRadio>
            <CustomRadio
                description="24/7 support. Contact us for pricing."
                value="enterprise"
            >
                Enterprise
            </CustomRadio>
        </RadioGroup>
    );
}
