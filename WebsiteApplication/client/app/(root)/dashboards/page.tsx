import Link from "next/link";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Dashboards | JettyDash",
};

export default async function Home({ }) {
  return (
    <div>
      <h1>Dashboards</h1>
    </div>
  );
}