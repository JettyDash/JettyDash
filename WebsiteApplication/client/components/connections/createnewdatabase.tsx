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
import {
    Tabs,
    Tab,
    Input,
    Link,
    Button,
    Card,
    CardBody,
    CardHeader,
    CardFooter,
    useDisclosure,
    Modal, ModalContent, ModalHeader, ModalBody, ModalFooter
} from "@nextui-org/react";
import React from "react";
import {Key} from "@react-types/shared";
import {DatabaseCards} from "@/components/connections/databasecards";
import {PlusIcon} from "@/components/icons/PlusIcon";
import {ConnectionForm} from "@/components/connections/connectionform";

export const CreateNewDatabase = () => {
    const {isOpen, onOpen, onOpenChange} = useDisclosure();


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
            <Modal className={"w-full h-full"}
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                placement="bottom-center" // center on desktop, bottom on mobile
            >
                <ModalContent className="flex items-center w-[440px] h-[610px]">
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">New Connection</ModalHeader>
                            <ModalBody className="w-full h-[400px] m-0 p-0 items-center">
                                <ConnectionForm/>

                            </ModalBody>
                            <ModalFooter className={"m-0 w-full"}>
                                <div className="w-full inline-flex flex-col gap-1">
                                    <Button
                                        onPress={onClose}
                                        isDisabled={false}
                                        className={"flex-2"}
                                        size={"lg"}
                                        variant="shadow"
                                        color="default" isLoading={false}
                                    >

                                        <span className={"text-small font-normal py-1"} >
                                            Test Connection
                                        </span>
                                    </Button>
                                    <Button
                                        onPress={onClose}
                                        isDisabled={false}
                                        className={"flex-2"}
                                        size={"lg"}
                                        variant="shadow"
                                        color="primary" isLoading={false}
                                    >
                                        <span className={"text-small font-normal py-1"} >
                                            Save Connection
                                        </span>
                                    </Button>

                                </div>

                            </ModalFooter>


                        </>
                    )}
                </ModalContent>
            </Modal>


    </>
            )
};