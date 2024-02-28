//
// import type {Metadata} from "next";
//
// export const metadata: Metadata = {
//     title: "Dashboards | JettyDash",
// };
//
// export default async function Dashboards({ }) {
//   return (
//     <div>
//       <h1>Dashboards</h1>
//     </div>
//   );
// }
//

'use client';
import {Tabs, Tab, Input, Link, Button, Card, CardBody, CardHeader, CardFooter, useDisclosure} from "@nextui-org/react";
import React from "react";
import {Key} from "@react-types/shared";
import {DatabaseCards} from "@/components/connections/databasecards";
import {PlusIcon} from "@/components/icons/PlusIcon";

export const CreateNewDatabase = () => {
    const [selectedTab, setSelectedTab] = React.useState<Key>("host");
    const [database, setDatabase] = React.useState<string>("")
    const [port, setPort] = React.useState<string>("1234"); // Add state for port number
    const {isOpen, onOpen, onOpenChange} = useDisclosure();

    const updatePort = (databaseType: string) => {
        switch (databaseType) {
            case "MYSQL":
                setPort("3306");
                break;
            case "POSTGRES":
                setPort("5432");
                break;
            case "MSSQL":
                setPort("1433");
                break;
            case "ORACLE":
                setPort("1521");
                break;
            default:
                setPort("1234");
                break;
        }
    };



    return (
        <>
            <Button
                onPress={onOpen}
                className="bg-foreground text-background"
                endContent={<PlusIcon/>}
                size="sm"
            >
                Add New
            </Button>

        <div className="flex flex-col w-full">
            <Card className="max-w-full w-[440px] h-[560px]">
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
                            <form className="flex flex-col gap-4">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setDatabase(selectedDatabase);
                                        updatePort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>
                                <div className={"flex flex-row gap-2"} >
                                    <Input
                                        className={"flex-2"}
                                        size="md"
                                        isRequired
                                        label="Database"
                                        placeholder="Enter your database name eg. master"
                                        type="text"
                                    />
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Port"
                                        placeholder={`eg. ${port}`}
                                        type="text"
                                    />
                                </div>
                                <div className={"flex"} >
                                    <Input
                                        className={"flex-1"}
                                        size="md"
                                        isRequired
                                        label="Host"
                                        placeholder="Enter your host eg. example-db.jettydash.com"
                                        type="text"
                                    />
                                </div>
                                <div className={"flex flex-row gap-2"} >
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Username"
                                        placeholder="Enter your username"
                                        type="text"/>
                                    <Input
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
                            <form className="flex flex-col gap-4 h-[300px]">
                                <Input isRequired label="Name" placeholder="Enter your names" type="password"/>
                                <Input isRequired label="Email" placeholder="Enter your email" type="email"/>
                                <Input
                                    isRequired
                                    label="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                />
                                <p className="text-center text-small">
                                    Already have an account?{" "}
                                    <Link size="sm" onPress={() => setSelectedTab("host")}>
                                        Host
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                    </Tabs>
                </CardBody>
                <CardFooter className={"m-0"}>
                    <div className="w-full inline-flex flex-col gap-1">
                        {/*<Button*/}
                        {/*    className={"flex-1"}*/}
                        {/*    size={"md"}*/}
                        {/*    variant="ghost"*/}

                        {/*>*/}
                        {/*    Cancel*/}
                        {/*</Button>*/}
                        {/*<div className={"flex flex-row gap-5"}>*/}
                        <Button
                            isDisabled={false}
                            className={"flex-2"}
                            size={"md"}
                            variant="shadow"
                            color="default" isLoading={false}
                        >
                            Test Connection
                        </Button>
                        <Button
                            isDisabled={false}
                            className={"flex-2"}
                            size={"md"}
                            variant="shadow"
                            color="primary" isLoading={false}
                        >
                            Save Connection
                        </Button>
                        {/*</div>*/}
                    </div>

                </CardFooter>
            </Card>
        </div>
    </>
            )
};