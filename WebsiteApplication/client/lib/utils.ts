import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";
var { parseConnectionString } = require('@tediousjs/connection-string');

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function parseDatabaseAndServer(connectionString:string) {
  // Parsing connection string
  // Getting connection options
  const connectionOptions = parseConnectionString(connectionString);

  let database, server;

  // Iterating over connection options to find database and server values
  for (const key in connectionOptions) {
    // Checking for database keys
    if (key.toLowerCase() === 'initial catalog' || key.toLowerCase() === 'database') {
      database = connectionOptions[key] ?? "";
    }
    // Checking for server keys
    else if (key.toLowerCase() === 'data source' || key.toLowerCase() === 'host' || key.toLowerCase() === 'server' || key.toLowerCase() === 'address' || key.toLowerCase() === 'addr' || key.toLowerCase() === 'network address') {
      server = connectionOptions[key] ?? "";
    }
  }

  return { database, server };
}

export function capitalize(str: string) {
  return str.charAt(0).toUpperCase() + str.slice(1);
}


interface FormattedDateTime {
  formattedDate: string;
  formattedTime: string;
  dayName: string;
}

export function formatDate(dateString: string): FormattedDateTime {
  const date = new Date(dateString);

  const formattedDate = `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;

  const hours = date.getHours().toString().padStart(2, '0');
  const minutes = date.getMinutes().toString().padStart(2, '0');
  const formattedTime = `${hours}:${minutes}`;

  const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  const dayName = days[date.getDay()];

  return {
    formattedDate,
    formattedTime,
    dayName
  };
}
