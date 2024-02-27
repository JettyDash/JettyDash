import React from 'react';
import {RadioGroup} from "@nextui-org/react";
import {CustomRadio} from "@/app/(root)/datamodels/customRadio";
import Image from "next/image";

const DatabaseCards = () => {
    return (
        <RadioGroup orientation="horizontal" label="Plans">
            <CustomRadio className={"group"} classNames={{labelWrapper: "m-0"}} value="MYSQL">
                <div className="flex flex-col items-center">
                    <Image className={"h-10 w-10 group-hover:animate-pulse dark:invert"} height={"40"} width={"40"}
                           src={"../assets/icons/mysql.svg"} alt={"mysql"}/>
                    <p className={"group-hover:animate-pulse"}>mysql</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{labelWrapper: "m-0"}} value="POSTGRES">
                <div className="flex flex-col items-center">
                    <Image className={"h-10 w-10 group-hover:animate-pulse dark:invert"} height={"40"} width={"40"}
                           src={"../assets/icons/postgres.svg"} alt={"postgres"}/>
                    <p className={"group-hover:animate-pulse"}>postgres</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{labelWrapper: "m-0"}} value="MSSQL">
                <div className="flex flex-col items-center">
                    <Image className={"h-10 w-10 group-hover:animate-pulse dark:invert"} height={"40"} width={"40"}
                           src={"../assets/icons/mssql.svg"} alt={"mssql"}/>
                    <p className={"group-hover:animate-pulse"}>mssql</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{labelWrapper: "m-0"}} value="ORACLE">
                <div className="flex flex-col items-center">
                    <Image className={"h-10 w-10 group-hover:animate-pulse dark:invert"} height={"40"} width={"40"}
                           src={"../assets/icons/oracle.svg"} alt={"oracle"}/>
                    <p className={"group-hover:animate-pulse"}>oracle</p>
                </div>
            </CustomRadio>
        </RadioGroup>
    );
};

export default Databased;