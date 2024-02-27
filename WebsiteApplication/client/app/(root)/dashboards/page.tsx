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
import {Tabs, Tab, Input, Link, Button, Card, CardBody, CardHeader} from "@nextui-org/react";
import React from "react";
import {Key} from "@react-types/shared";
import Image from "next/image";
import {Database} from "lucide-react";
import {DatabaseCards} from "@/components/connections/databasecards";
export default function Dashboards({}) {
    const [selected, setSelected] = React.useState<Key>("login");

    return (
        <div className="flex flex-col w-full">
            <Card className="max-w-full w-[440px] h-[600px]">
                <CardHeader>
                    <Image className={"dark:invert"} width={"32"} height={"32"} src={"../../../assets/icons/global.svg"} alt={"global"}/>
                </CardHeader>
                <CardBody className="overflow-hidden">
                    <Tabs
                        fullWidth
                        variant="bordered"
                        size="md"
                        aria-label="Tabs form"
                        selectedKey={selected}
                        onSelectionChange={setSelected}
                    >
                        <Tab key="login" title="Login">
                            <form className="flex flex-col gap-4">
                                <DatabaseCards imageSizeClasses={"h-10 w-10"} baseCardClasses={"size-24"} />
                                <Input isRequired label="Email" placeholder="Enter your email" type="email"/>
                                <Input
                                    isRequired
                                    label="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                />
                                <p className="text-center text-small">
                                    Need to create an account?{" "}
                                    <Link size="sm" onPress={() => setSelected("sign-up")}>
                                        Sign up
                                    </Link>
                                </p>
                                <div className="flex gap-2 justify-end">
                                    <Button fullWidth color="primary">
                                        Logins
                                    </Button>
                                </div>
                            </form>
                        </Tab>
                        <Tab key="sign-up" title="Sign up">
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
                                    <Link size="sm" onPress={() => setSelected("login")}>
                                        Login
                                    </Link>
                                </p>
                                <div className="flex gap-2 justify-end">
                                    <Button fullWidth color="primary">
                                        Sign up
                                    </Button>
                                </div>
                            </form>
                        </Tab>
                    </Tabs>
                </CardBody>
            </Card>
        </div>
    )
};