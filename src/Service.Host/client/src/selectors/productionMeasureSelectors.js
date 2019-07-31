export const getCitsData = state => {
    const { menu } = state;
    if (!menu.data) return null;
    return menu.data.myStuff;
};

export const getReportLoading = state => {
    const menu = state;
    return menu ? menu.loading : false;
};
