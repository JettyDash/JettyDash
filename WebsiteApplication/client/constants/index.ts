import {IDatabaseType, SidebarLink} from "@/types";
import {UnknownDbIcon} from "@/components/icons/UnknownDbIcon";
import MySqlDbIcon from "@/components/icons/MySqlDbIcon";
import PostgresDbIcon from "@/components/icons/PostgresDbIcon";
import OracleDbIcon from "@/components/icons/OracleDbIcon";
import MsSqlDbIcon from "@/components/icons/MsSqlDbIcon";



export const themes = [
  { value: "light", label: "Light", icon: "/assets/icons/sun.svg" },
  { value: "dark", label: "Dark", icon: "/assets/icons/moon.svg" },
  { value: "system", label: "System", icon: "/assets/icons/computer.svg" },
];


export const DatabaseType: IDatabaseType = {
    "MYSQL": MySqlDbIcon,
    "POSTGRES": PostgresDbIcon,
    "ORACLE": OracleDbIcon,
    "MSSQL": MsSqlDbIcon,
    "UNKNOWN": UnknownDbIcon
};


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


export const BADGE_CRITERIA = {
  QUESTION_COUNT: {
    BRONZE: 10,
    SILVER: 50,
    GOLD: 100,
  },
  ANSWER_COUNT: {
    BRONZE: 10,
    SILVER: 50,
    GOLD: 100,
  },
  QUESTION_UPVOTES: {
    BRONZE: 10,
    SILVER: 50,
    GOLD: 100,
  },
  ANSWER_UPVOTES: {
    BRONZE: 10,
    SILVER: 50,
    GOLD: 100,
  },
  TOTAL_VIEWS: {
    BRONZE: 1000,
    SILVER: 10000,
    GOLD: 100000,
  },
};
