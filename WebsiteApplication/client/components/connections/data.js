
const statusOptions = [
    {name: "Active", uid: "active"},
    {name: "Inactive", uid: "inactive"},
    {name: "Paused", uid: "Paused"},
];
const connections = [
    {
        id: 0,
        name: "Real Time fv",
        databaseType: "FIREBIRD",
        status: "active",
        date: "25",
    },
    {
        id: 1,
        name: "active_users_only",
        databaseType: "POSTGRES",
        status: "active",
        date: "29",
    },
    {
        id: 2,
        name: "not_so_active_users",
        databaseType: "POSTGRES",
        status: "paused",
        date: "25",
    },
    {
        id: 3,
        name: "players",
        databaseType: "MSSQL",
        status: "active",
        date: "22",
    },
    {
        id: 4,
        name: "nobody_cares_about_this_database",
        databaseType: "MYSQL",
        status: "inactive",
        date: "20",
    },
    {
        id: 5,
        name: "rfma",
        databaseType: "ORACLE",
        status: "active",
        date: "29",
    },
    {
        id: 6,
        name: "kelebekler",
        databaseType: "MYSQL",
        status: "paused",
        date: "25",
    }
];


const columns = [
    {name: "ID", uid: "id", sortable: true},
    {name: "NAME", uid: "name", sortable: true},
    {name: "Date", uid: "date", sortable: true},
    {name: "STATUS", uid: "status", sortable: true},
    {name: "ACTIONS", uid: "actions"},
];

export {columns, connections, statusOptions};
