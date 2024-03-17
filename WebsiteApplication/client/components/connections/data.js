const statusOptions = [
  { name: "Active", uid: "active" },
  { name: "Inactive", uid: "inactive" },
  { name: "Paused", uid: "Paused" }
];
const connections = [
  {
    id: 0,
    name: "Real Time fv",
    databaseType: "FIREBIRD",
    status: "active",
    date: "2023-08-12T20:13:46.384Z"
  },
  {
    id: 1,
    name: "active_users_only",
    databaseType: "POSTGRES",
    status: "active",
    date: "2024-01-12T20:15:46.384Z"
  },
  {
    id: 2,
    name: "not_so_active_users",
    databaseType: "POSTGRES",
    status: "paused",
    date: "2023-09-11T20:14:56.384Z"
  },
  {
    id: 3,
    name: "players",
    databaseType: "MSSQL",
    status: "active",
    date: "2022-09-11T20:22:22.384Z"
  },
  {
    id: 4,
    name: "nobody_cares_about_this_database",
    databaseType: "MYSQL",
    status: "inactive",
    date: "2021-01-09T20:22:22.384Z"
  },
  {
    id: 5,
    name: "rfma",
    databaseType: "ORACLE",
    status: "active",
    date: "2011-01-09T20:22:22.384Z"
  },
  {
    id: 6,
    name: "kelebekler",
    databaseType: "MYSQL",
    status: "paused",
    date: "2008-01-09T20:22:22.384Z"
  }
];


const columns = [
  { name: "ID", uid: "id", sortable: true },
  { name: "NAME", uid: "name", sortable: true },
  { name: "DATE", uid: "date", sortable: true },
  { name: "STATUS", uid: "status", sortable: true },
  { name: "ACTIONS", uid: "actions" }
];

export { columns, connections, statusOptions };
