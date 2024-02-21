import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Home | JettyDash",
};

export default async function Home({ }) {
  // const { userId } = auth();
  const userId = "1";

  let result;

  return (
    <div>
      <h1>Connections</h1>
      <p>Welcome to JettyDash</p>
      <Link href={"google.com"}>Posts</Link>
    </div>
  );
}
