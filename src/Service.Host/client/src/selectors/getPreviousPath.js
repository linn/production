const getPreviousPath = state => {
    const realPaths = state.historyStore?.filter(path => !path.includes('signin-oidc-client')); // ignore oidc path
    return realPaths[realPaths.length - 1] || '/production'; // just go home if real location in history
};

export default getPreviousPath;
