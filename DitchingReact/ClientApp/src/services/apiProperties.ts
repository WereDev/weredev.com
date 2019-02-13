class ApiProperties {
    public getBaseUrl() : string {
        const protocol = window.location.protocol;
        const host = window.location.hostname;
        return protocol + "//" + host + "/api/";
    }
}

export default new ApiProperties();