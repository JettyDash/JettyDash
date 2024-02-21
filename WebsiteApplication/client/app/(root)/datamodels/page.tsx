import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "DataModels | JettyDash",
};

export default async function Home({ }) {
  return (
    <div>
      <h1>DataModels</h1>
    </div>
  );
}
