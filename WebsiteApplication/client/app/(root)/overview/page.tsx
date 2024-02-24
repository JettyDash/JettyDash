import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Overview",
};

export default async function Overview({ }) {
  return (
    <div>
      <h1>Overview</h1>
      <p>Welcome to JettyDash</p>
      <Link href={"google.com"}>Posts</Link>
    </div>
  );
}
