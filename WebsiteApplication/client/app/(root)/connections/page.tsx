import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Connections | JettyDash",
};

export default async function Home({ }) {
  return (
    <div>
      <h1>Connections</h1>
      <Link href={"google.com"}>Posts</Link>
    </div>
  );
}
