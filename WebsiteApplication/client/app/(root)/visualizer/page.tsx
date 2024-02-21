import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Visualizer | JettyDash",
};

export default async function Home({ }) {
  // const { userId } = auth();
  return (
    <div>
      <h1>Visualizer</h1>
    </div>
  );
}
