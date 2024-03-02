"use client";
import {
		Button,
		Modal,
		ModalBody,
		ModalContent,
		ModalFooter,
		ModalHeader,
		Spinner,
		useDisclosure
} from "@nextui-org/react";
import React from "react";
import { PlusIcon } from "@/components/icons/PlusIcon";
import { ConnectionForm } from "@/components/forms/connectionform";
import {
		useHostFormButtonStore,
		useHostFormStore,
		useTabStore,
		useUrlFormStore
} from "@/lib/stores/connectionformstore";
import { toast } from "sonner";
import { testHostConnectionRequest } from "@/lib/actions/connection.action";


export const CreateNewDatabase = () => {
		const { isOpen, onOpen, onOpenChange } = useDisclosure();
		const [selectedTab] = useTabStore(state => [state.selectedTab, state.setSelectedTab]);
		const [isHostFormValid] = useHostFormStore(state => [state.isHostFormValid]);
		const [isHostFormTestButtonLoading, isHostFormSaveButtonLoading] = useHostFormButtonStore(state => [state.isHostFormTestButtonLoading, state.isHostFormSaveButtonLoading]);
		const [isUrlFormValid] = useUrlFormStore(state => [state.isUrlFormValid]);
		const { ...params } = useHostFormStore.getState();

		let handleIsValid = () => {
				if (selectedTab === "host") {
						return isHostFormValid();
				} else {
						return isUrlFormValid();
				}
		};

		const handleTestConnection = async () => {
				try {
						useHostFormButtonStore.setState({ isHostFormTestButtonLoading: true });
						const promise = await testHostConnectionRequest({ ...params });
						useHostFormButtonStore.setState({ isHostFormTestButtonLoading: false });
				} catch (error) {
						toast.error("Connection Failed " + error);
						useHostFormButtonStore.setState({ isHostFormTestButtonLoading: false });
				}
		};

		return (
				<>
						<Button
								onPress={onOpen}
								className="bg-foreground text-background"
								endContent={<PlusIcon />}
								size="sm"
						>
								Add New
						</Button>
						<Modal
								className={"w-full h-full"}
								motionProps={{
										variants: {
												enter: {
														y: 0,
														opacity: 1,
														transition: {
																duration: 0.3,
																ease: "easeOut"
														}
												},
												exit: {
														y: -20,
														opacity: 0,
														transition: {
																duration: 0.2,
																ease: "easeIn"
														}
												}
										}
								}}
								isOpen={isOpen}
								onOpenChange={onOpenChange}
								placement="bottom-center" // center on desktop, bottom on mobile
						>
								<ModalContent className="flex items-center w-[440px] h-[630px]">
										{(onClose) => (
												<>
														<ModalHeader className="flex flex-col gap-1">New Connection</ModalHeader>
														<ModalBody className="w-full h-[400px] m-0 p-0 items-center">
																<ConnectionForm />

														</ModalBody>
														<ModalFooter className={"m-0 w-full"}>
																<div className="w-full inline-flex flex-col gap-1">
																		<Button
																				// spinner={<Spinner size={"sm"} labelColor="foreground"/>}
																				spinner={<Spinner size={"sm"} color="current" />}
																				type={"submit"}
																				onPress={() => {
																						onClose();
																				}}
																				isLoading={false}
																				isDisabled={!handleIsValid()}
																				className={"flex-2"}
																				size={"md"}
																				variant="ghost"
																				color={"secondary"}
																		>

                                        <span className={"text-small font-normal py-1"}>
                                            Try an Example Connection
                                        </span>
																		</Button>
																		<Button
																				// spinner={<Spinner size={"sm"} labelColor="foreground"/>}
																				spinner={<Spinner size={"sm"} color="current" />}
																				type={"submit"}
																				onPress={async () => {
																						await handleTestConnection();
																						// onClose();
																				}}
																				isLoading={isHostFormTestButtonLoading}
																				isDisabled={!handleIsValid()}
																				className={"flex-2"}
																				size={"lg"}
																				variant="shadow"
																				color="default"
																		>

                    <span className={"text-small font-normal py-1"}>
                        Test Connection
                    </span>
																		</Button>
																		<Button
																				spinner={<Spinner size={"sm"} color="current" />}
																				type={"submit"}
																				onPress={onClose}
																				isLoading={isHostFormSaveButtonLoading}
																				isDisabled={!handleIsValid()}
																				className={"flex-2"}
																				size={"lg"}
																				variant="shadow"
																				color="primary"
																		>
                                        <span className={"text-small font-normal py-1"}>
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
		);
};