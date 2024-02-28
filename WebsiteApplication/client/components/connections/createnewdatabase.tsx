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
            <Modal
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                placement="bottom-center" // center on desktop, bottom on mobile
            >
                <ModalContent className="flex items-center w-[800px] h-[600px]">
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">New Connection</ModalHeader>
                            <ModalBody className="w-full h-full">

                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="flat" onPress={onClose}>
                                    Close
                                </Button>
                                <Button onPress={onClose}>
                                    Sign in
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>


    </>
            )
};