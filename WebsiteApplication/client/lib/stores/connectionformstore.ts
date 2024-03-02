import { create } from 'zustand';
import {Key} from "@react-types/shared";

interface TabStoreState {
    selectedTab: Key;
    setSelectedTab: (key: Key) => void;
}

export const useTabStore = create<TabStoreState>((set) => ({
    selectedTab: 'host', // Initial selected tab
    setSelectedTab: (value) => set({ selectedTab: value }),
}));

interface HostFormData {
    HostDatabaseName: string,
    HostDatabaseType: string,
    HostUsername: string,
    HostPassword: string,
    Host: string,
    Port: string,
}

interface HostFormLoadingState {
    isHostFormTestButtonLoading: boolean,
    setHostFormTestButtonLoading: (value: boolean) => void,
    isHostFormSaveButtonLoading: boolean,
    setHostFormSaveButtonLoading: (value: boolean) => void,
}

export const useHostFormButtonStore = create<HostFormLoadingState>((set) => ({
    isHostFormTestButtonLoading: false,
    setHostFormTestButtonLoading: (value) => set({ isHostFormTestButtonLoading: value }),
    isHostFormSaveButtonLoading: false,
    setHostFormSaveButtonLoading: (value) => set({ isHostFormSaveButtonLoading: value }),
}));

interface UrlFormData {
    UrlDatabaseName: string,
    UrlDatabaseType: string,
    UrlUsername: string,
    UrlPassword: string,
    Url: string,
}

// Define Zustand store for host form data
interface HostFormStoreState extends HostFormData {
    setHostDatabaseName: (value: string) => void,
    setHostDatabaseType: (value: string) => void,
    setHostUsername: (value: string) => void,
    setHostPassword: (value: string) => void,
    setHost: (value: string) => void,
    setPort: (value: string) => void,
    isHostFormValid: () => boolean,
    // testHostConnection: () => Promise<void>,
}

export const useHostFormStore = create<HostFormStoreState>((set, get) => ({
    HostDatabaseName: '',
    HostDatabaseType: '',
    HostUsername: '',
    HostPassword: '',
    Host: '',
    Port: '1234', // Initial port value
    setHostDatabaseName: (value) => set({ HostDatabaseName: value }),
    setHostDatabaseType: (value) => set({ HostDatabaseType: value }),
    setHostUsername: (value) => set({ HostUsername: value }),
    setHostPassword: (value) => set({ HostPassword: value }),
    setHost: (value) => set({ Host: value }),
    setPort: (value) => set({ Port: value }),

    isHostFormValid: () => {
        const { HostDatabaseName, HostDatabaseType, HostUsername, HostPassword, Host, Port } = get();
        return (
            HostDatabaseName.trim() !== '' &&
            HostDatabaseType.trim() !== '' &&
            HostUsername.trim() !== '' &&
            HostPassword.trim() !== '' &&
            Host.trim() !== '' &&
            Port.trim() !== ''
        );
    }
}));

// Define Zustand store for URL form data
interface UrlFormStoreState extends UrlFormData {
    setUrlDatabaseName: (value: string) => void,
    setUrlDatabaseType: (value: string) => void,
    setUrlUsername: (value: string) => void,
    setUrlPassword: (value: string) => void,
    setUrl: (value: string) => void,
    isUrlFormValid: () => boolean,
    /*TODO:is Loading*/
}

export const useUrlFormStore = create<UrlFormStoreState>((set, get) => ({
    UrlDatabaseName: '',
    UrlDatabaseType: '',
    UrlUsername: '',
    UrlPassword: '',
    Url: '',

    setUrlDatabaseName: (value) => set({ UrlDatabaseName: value }),
    setUrlDatabaseType: (value) => set({ UrlDatabaseType: value }),
    setUrlUsername: (value) => set({ UrlUsername: value }),
    setUrlPassword: (value) => set({ UrlPassword: value }),
    setUrl: (value) => set({ Url: value }),

    isUrlFormValid: () => {
        const { UrlDatabaseName, UrlDatabaseType, UrlUsername, UrlPassword, Url } = get();
        return (
            UrlDatabaseType.trim() !== '' &&
            UrlUsername.trim() !== '' &&
            UrlPassword.trim() !== '' &&
            Url.trim() !== ''
        );
    },


}));
