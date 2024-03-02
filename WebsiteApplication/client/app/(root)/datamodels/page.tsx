"use client";
import React from "react";
import { Button, useDisclosure } from "@nextui-org/react";
import { PlusIcon } from "@/components/icons/PlusIcon";
import { Key } from "@react-types/shared";


// tst
export default function DataModels() {
		const { isOpen, onOpen, onOpenChange } = useDisclosure();
		const [selected, setSelected] = React.useState<Key>("login");

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
				</>
		);
}
