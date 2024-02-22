import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "JettyDash",
};

export default async function Home({ }) {
  return (
    <div>
      <h1>Home</h1>
      <p>Welcome to Dev Overflow</p>
      <Link href={"google.com"}>Posts</Link>
    </div>
  );
}
