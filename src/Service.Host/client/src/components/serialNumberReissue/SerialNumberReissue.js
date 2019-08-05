import React, { useState, useEffect, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    InputField,
    Loading,
    SaveBackCancelButtons,
    SearchInputField,
    SnackbarMessage,
    Title,
    TypeaheadDialog,
    ErrorCard,
    useSearch,
    sortList
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    searchIcon: {
        paddingTop: theme.spacing(2)
    },
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

function SerialNumberReissue({
    addItem,
    editStatus,
    errorMessage,
    fetchSerialNumbers,
    fetchSalesArticle,
    history,
    loading,
    salesArticle,
    setEditStatus,
    serialNumbers,
    serialNumbersLoading,
    snackbarVisible,
    setSnackbarVisible,
    salesArticleSearchResults,
    salesArticlesSearchLoading,
    clearSalesArticlesSearch,
    searchSalesArticles
}) {
    const [searchTerm, setSearchTerm] = useState(null);
    const [sernosGroups, setSernosGroups] = useState([]);
    const [selectedSernosGroup, setSelectedSernosGroup] = useState('');
    const [selectedSerialNumber, setSelectedSerialNumber] = useState(null);

    const classes = useStyles();

    useSearch(fetchSerialNumbers, searchTerm, null, 'sernosNumber');

    useEffect(() => {
        if (serialNumbers && serialNumbers.length) {
            // TODO try doing this on one line
            // or could use reduce...
            const groups = [];
            serialNumbers.map(
                sernos => !groups.includes(sernos.sernosGroup) && groups.push(sernos.sernosGroup)
            );
            const sortedGroups = sortList(groups);
            setSernosGroups(sortedGroups);
            setSelectedSernosGroup(sortedGroups[0] || '');
        }
    }, [serialNumbers]);

    // TODO convert this to function?
    useEffect(() => {
        const sernos = serialNumbers.find(s => s.sernosGroup === selectedSernosGroup);
        setSelectedSerialNumber(sernos);
        if (sernos) {
            fetchSalesArticle(sernos.articleNumber);
        }
    }, [selectedSernosGroup, serialNumbers, fetchSalesArticle]);

    const viewing = () => editStatus === 'view';

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (propertyName === 'searchTerm') {
            setSearchTerm(newValue);
            return;
        }
        if (propertyName === 'selectedSalesArticle') {
            setSelectedSernosGroup(newValue);
            return;
        }
        setSelectedSerialNumber({ ...selectedSerialNumber, [propertyName]: newValue });
    };

    const handleNewArticleNumberSelect = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setSelectedSerialNumber({
            ...selectedSerialNumber,
            newArticleNumber: newValue.articleNumber,
            newArticleNumberDescription: newValue.description
        });
    };

    const handleSaveClick = () => {
        addItem(selectedSerialNumber);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/ate/fault-codes/');
    };

    return (
        <Page>
            <Grid container spacing={3}>
                {errorMessage && <ErrorCard errorMessage={errorMessage} />}
                <Grid item xs={12}>
                    <Title text="Reissue Serial Numbers" />
                </Grid>
                <Grid item xs={3}>
                    <SearchInputField
                        label="Search by Serial Number"
                        fullWidth
                        placeholder="Serial Number"
                        onChange={handleFieldChange}
                        propertyName="searchTerm"
                        type="number"
                        value={searchTerm}
                    />
                </Grid>
                <Grid item xs={9} />
                {loading || serialNumbersLoading ? (
                    <Loading />
                ) : (
                    serialNumbers.length > 0 && (
                        <Fragment>
                            <Grid container spacing={3} className={classes.marginTop}>
                                {/* TODO bring back margin top */}
                                <Grid item xs={3}>
                                    <Dropdown
                                        value={selectedSernosGroup || ''}
                                        label="Filter by Sernos Group"
                                        fullWidth
                                        items={sernosGroups.length ? sernosGroups : ['']}
                                        onChange={handleFieldChange}
                                        propertyName="selectedSalesArticle"
                                    />
                                </Grid>
                                <Grid item xs={9} />
                                {selectedSerialNumber && (
                                    <Fragment>
                                        <SnackbarMessage
                                            visible={snackbarVisible}
                                            onClose={() => setSnackbarVisible(false)}
                                            message="Save Successful"
                                        />
                                        <Grid item xs={3}>
                                            <InputField
                                                disabled
                                                label="Article Number"
                                                type="string"
                                                propertyName="articleNumber"
                                                value={selectedSerialNumber.articleNumber}
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={1} />
                                        <Grid item xs={5}>
                                            <InputField
                                                disabled
                                                label="Description"
                                                type="string"
                                                propertyName="articleNumberDescription"
                                                value={salesArticle ? salesArticle.description : ''}
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={3} />
                                        <Grid item xs={3}>
                                            <InputField
                                                label="New Article Number"
                                                disabled
                                                type="string"
                                                propertyName="newArticleNumber"
                                                value={selectedSerialNumber.newArticleNumber}
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={1}>
                                            <div className={classes.searchIcon}>
                                                <TypeaheadDialog
                                                    title="Sales Article Search"
                                                    onSelect={handleNewArticleNumberSelect}
                                                    searchItems={salesArticleSearchResults}
                                                    loading={salesArticlesSearchLoading}
                                                    fetchItems={searchSalesArticles}
                                                    clearSearch={clearSalesArticlesSearch}
                                                />
                                            </div>
                                        </Grid>
                                        <Grid item xs={5}>
                                            <InputField
                                                disabled
                                                label="Description"
                                                type="string"
                                                propertyName="newArticleNumberDescription"
                                                value={
                                                    selectedSerialNumber.newArticleNumberDescription
                                                }
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={3} />
                                        <Grid item xs={3}>
                                            <InputField
                                                label="Comments"
                                                type="string"
                                                rows={2}
                                                propertyName="comments"
                                                value={selectedSerialNumber.comments}
                                                onChange={handleFieldChange}
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={9} />
                                        <Grid item xs={3}>
                                            <InputField
                                                disabled
                                                label="New Serial Number"
                                                type="number"
                                                propertyName="newSerialNumber"
                                                value={selectedSerialNumber.newSerialNumber}
                                                onChange={handleFieldChange}
                                                fullWidth
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <SaveBackCancelButtons
                                                saveDisabled={viewing()}
                                                saveClick={handleSaveClick}
                                                cancelClick={handleBackClick}
                                                backClick={handleBackClick}
                                            />
                                        </Grid>
                                    </Fragment>
                                )}
                            </Grid>
                        </Fragment>
                    )
                )}
            </Grid>
        </Page>
    );
}

SerialNumberReissue.propTypes = {
    addItem: PropTypes.func.isRequired,
    editStatus: PropTypes.func.isRequired,
    errorMessage: PropTypes.string,
    fetchSerialNumbers: PropTypes.func.isRequired,
    fetchSalesArticle: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    salesArticle: PropTypes.shape({}),
    setEditStatus: PropTypes.func.isRequired,
    serialNumbers: PropTypes.shape({}),
    serialNumbersLoading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    salesArticleSearchResults: PropTypes.shape({}),
    salesArticlesSearchLoading: PropTypes.bool,
    clearSalesArticlesSearch: PropTypes.func.isRequired,
    searchSalesArticles: PropTypes.func.isRequired
};

SerialNumberReissue.defaultProps = {
    errorMessage: '',
    loading: false,
    salesArticle: null,
    serialNumbers: null,
    serialNumbersLoading: false,
    snackbarVisible: false,
    salesArticleSearchResults: null,
    salesArticlesSearchLoading: false
};

export default SerialNumberReissue;
