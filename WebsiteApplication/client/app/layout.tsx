import React from "react";
import { Ubuntu } from "next/font/google";
import type { Metadata } from "next";
import "./globals.css";
import { Providers } from "./providers";
import { SpeedInsights } from "@vercel/speed-insights/next";
import { Toaster } from "sonner";
import Loader from "@/components/icons/LoadingIcon";
import { Analytics } from "@vercel/analytics/next";


const ubuntu = Ubuntu({
		subsets: ["latin"],
		weight: ["300", "400", "500", "700"], // Include all the weights you defined
		style: ["normal", "italic"], // Include both normal and italic styles
		variable: "--font-ubuntu"
});

export const metadata: Metadata = {
		title: "JettyDash",
		description:
				"JettyDash is a modern, open-source dashboard creator for your business.",
		icons: {
				icon: "/assets/images/site-logo.svg"
		}
};

export default function RootLayout({ children }: {
		children: React.ReactNode;
}) {
		return (
				<html lang="en" suppressHydrationWarning>

				<body className={`${ubuntu.variable}`}>

				<Providers>
						{children}
						<Toaster position="bottom-right"
						         richColors={true}
						         icons={{
								         loading: <Loader visible={true} />
						         }}
						/>
						<SpeedInsights />
						<Analytics />
				</Providers>


				</body>
				</html>
		);
}
