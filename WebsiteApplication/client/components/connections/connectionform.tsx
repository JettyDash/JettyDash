import {Card, CardBody, Input, Link, Tab, Tabs, Textarea} from "@nextui-org/react";
import {DatabaseCards} from "@/components/connections/databasecards";
import React from "react";
import {parseDatabaseAndServer} from "@/lib/utils";
import {useUrlFormStore, useHostFormStore, useTabStore} from "@/lib/stores/connectionformstore";

export const ConnectionForm = () => {
    const [selectedTab, setSelectedTab] = useTabStore(state => [state.selectedTab, state.setSelectedTab]);
    const [defaultPort, setDefaultPort] = React.useState<string>("1234");
    const [parsedServerFromUrl, setParsedServerFromUrl] = React.useState<string>("null");
    const [parsedDatabaseFromUrl, setParsedDatabaseFromUrl] = React.useState<string>("null");
    const [
        setHostDatabaseName,
        setHostDatabaseType,
        setHostUsername,
        setHostPassword,
        setHost,
        setPort
    ] = useHostFormStore(state => [
        state.setHostDatabaseName,
        state.setHostDatabaseType,
        state.setHostUsername,
        state.setHostPassword,
        state.setHost,
        state.setPort
    ]);

    const [
        Url,
        setUrlDatabaseName,
        setUrlDatabaseType,
        setUrlUsername,
        setUrlPassword,
        setUrl
    ] = useUrlFormStore(state => [
        state.Url,
        state.setUrlDatabaseName,
        state.setUrlDatabaseType,
        state.setUrlUsername,
        state.setUrlPassword,
        state.setUrl
    ]);

    const updateDefaultPort = (databaseType: string) => {
        switch (databaseType) {
            case "MYSQL":
                setDefaultPort("3306");
                break;
            case "POSTGRES":
                setDefaultPort("5432");
                break;
            case "MSSQL":
                setDefaultPort("1433");
                break;
            case "ORACLE":
                setDefaultPort("1521");
                break;
            default:
                setDefaultPort("1234");
                break;
        }
    };

    return (
        <div className="flex flex-col">
            <Card radius={"none"} shadow={"none"} className="max-w-full w-[440px] h-[560px]">
                <CardBody className="overflow-hidden">
                    <Tabs
                        fullWidth
                        variant="bordered"
                        size="sm"
                        aria-label="Tabs form"
                        selectedKey={selectedTab}
                        onSelectionChange={setSelectedTab}
                    >

                        <Tab key="host" title="Host">
                            <form className="flex flex-col gap-3">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setHostDatabaseType(selectedDatabase);
                                        updateDefaultPort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>
                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        onValueChange={(value: string) => setHostDatabaseName(value)}
                                        className={"flex-2"}
                                        size="md"
                                        isRequired
                                        label="Database"
                                        placeholder="Enter your database name eg. master"
                                        type="text"
                                    />
                                    <Input
                                        onValueChange={(value: string) => setPort(value)}
                                        className={"flex-1"}
                                        isRequired
                                        label="Port"
                                        placeholder={`eg. ${defaultPort}`}
                                        type="text"
                                    />
                                </div>
                                <div className={"flex"}>
                                    <Input
                                        onValueChange={(value: string) => setHost(value)}
                                        className={"flex-1"}
                                        size="md"
                                        isRequired
                                        label="Host"
                                        placeholder="Enter your host eg. example-db.jettydash.com"
                                        type="text"
                                    />
                                </div>
                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        onValueChange={(value: string) => setHostUsername(value)}
                                        className={"flex-1"}
                                        isRequired
                                        label="Username"
                                        placeholder="Enter your username"
                                        type="text"/>
                                    <Input
                                        onValueChange={(value: string) => setHostPassword(value)}
                                        className={"flex-1"}
                                        isRequired
                                        label="Password"
                                        placeholder="Enter your password"
                                        type="password"
                                    />
                                </div>

                                <p className="text-center text-small ">
                                    Need a connection example?{" "}
                                    <Link size="sm" onPress={() => setSelectedTab("Url")}>
                                        Learn More
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                        <Tab key="url" title="Url">
                            <form className="flex flex-col gap-3">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setUrlDatabaseType(selectedDatabase);
                                        updateDefaultPort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>

                                <div className={"flex flex-col h-[124px]"}>
                                    <Textarea
                                        rows={3}
                                        isRequired
                                        label="Url"
                                        value={Url}
                                        onValueChange={(value: string) => {
                                            setUrl(value);
                                            const {database, server} = parseDatabaseAndServer(value);
                                            setUrlDatabaseName(database);
                                            setParsedDatabaseFromUrl(database);
                                            setParsedServerFromUrl(server);
                                        }}

                                        placeholder="Enter your url eg. Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;"
                                        className="flex-1"
                                    />
                                    <p className="text-default-500 text-small">Database: {parsedDatabaseFromUrl ?? "not found"} Host: {parsedServerFromUrl ?? "not found"}</p>
                                </div>

                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        onValueChange={(value: string) => setUrlUsername(value)}
                                        className={"flex-1"}
                                        isRequired
                                        label="Username"
                                        placeholder="Enter your username"
                                        type="text"/>
                                    <Input
                                        onValueChange={(value: string) => setUrlPassword(value)}
                                        className={"flex-1"}
                                        isRequired
                                        label="Password"
                                        placeholder="Enter your password"
                                        type="password"
                                    />
                                </div>

                                <p className="text-center text-small ">
                                    Need a connection example?{" "}
                                    <Link size="sm" onPress={() => setSelectedTab("Host")}>
                                        Learn More
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                    </Tabs>
                </CardBody>

            </Card>
        </div>
    );
}