import { SidebarLink } from "@/types";

export const themes = [
  { value: "light", label: "Light", icon: "/assets/icons/sun.svg" },
  { value: "dark", label: "Dark", icon: "/assets/icons/moon.svg" },
  { value: "system", label: "System", icon: "/assets/icons/computer.svg" },
];

export const sidebarLinks: SidebarLink[] = [
  {
    route: "/overview",
    label: "Overview",
  },
  {
    route: "/dashboards",
    label: "Dashboards",
  },
  {
    route: "/visualizer",
    label: "Visualizer",
  },
  {
    route: "/datamodels",
    label: "DataModels",
  },
  {
    route: "/connections",
    label: "Connections",
  },
  {
    route: "/settings",
    label: "Settings",
  },
];

