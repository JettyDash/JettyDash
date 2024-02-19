import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Home | Dev Overflow",
};

export default async function Home({ }) {
  // const { userId } = auth();
  const userId = "1";

  let result;

  return (
    <div>
      <h1>Connection</h1>
      <p>Welcome to Dev Overflow</p>
      <Link href={"google.com"}>Posts</Link>
    </div>
  );
}
