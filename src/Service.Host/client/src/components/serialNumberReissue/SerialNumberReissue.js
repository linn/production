import React, { useState, useCallback, useEffect, Fragment } from 'react';
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
    utilities
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
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
    itemErrors,
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
    searchSalesArticles,
    reissuedSerialNumber,
    options
}) {
    const [searchTerm, setSearchTerm] = useState(null);
    const [sernosGroups, setSernosGroups] = useState([]);
    const [selectedSernosGroup, setSelectedSernosGroup] = useState('');
    const [selectedSerialNumber, setSelectedSerialNumber] = useState(null);

    const classes = useStyles();

    useSearch(fetchSerialNumbers, searchTerm, null, 'sernosNumber');

    const selectSerialNumber = useCallback(
        sernosGroup => {
            const sernos = serialNumbers.find(s => s.sernosGroup === sernosGroup);
            setSelectedSerialNumber({ ...sernos, newSerialNumber: null });
            if (sernos) {
                fetchSalesArticle(sernos.articleNumber);
            }
        },
        [fetchSalesArticle, serialNumbers]
    );

    useEffect(() => {
        if (options.serialNumber) {
            setSearchTerm(options.serialNumber);
        }
    }, [options]);

    useEffect(() => {
        if (serialNumbers && serialNumbers.length) {
            const sortedGroups = serialNumbers.reduce((groups, serialNumber) => {
                if (!groups.includes(serialNumber.sernosGroup)) {
                    groups.push(serialNumber.sernosGroup);
                }
                return utilities.sortList(groups);
            }, []);

            setSernosGroups(sortedGroups);
            setSelectedSernosGroup(sortedGroups[0] || '');
            selectSerialNumber(sortedGroups[0]);
        }
    }, [serialNumbers, selectSerialNumber]);

    useEffect(() => {
        if (reissuedSerialNumber) {
            setSelectedSerialNumber(sernos => ({
                ...sernos,
                newSerialNumber: reissuedSerialNumber.newSerialNumber
            }));
        }
    }, [reissuedSerialNumber]);

    const viewing = () => editStatus === 'view';

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (propertyName === 'searchTerm') {
            setSearchTerm(newValue);
            return;
        }
        if (propertyName === 'selectedSernosGroup') {
            setSelectedSernosGroup(newValue);
            selectSerialNumber(newValue);
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
        addItem({
            sernosGroup: selectedSerialNumber.sernosGroup,
            serialNumber: selectedSerialNumber.sernosNumber,
            articleNumber: selectedSerialNumber.articleNumber,
            newArticleNumber: selectedSerialNumber.newArticleNumber,
            comments: selectedSerialNumber.comments
        });
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/ate/fault-codes/');
    };

    const handleCancelClick = () => {
        setSelectedSerialNumber(reissuedSerialNumber);
        setSelectedSernosGroup('');
        setSearchTerm('');
        setEditStatus('view');
    };

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Reissue Serial Numbers" />
                </Grid>
                {itemErrors &&
                    !loading &&
                    itemErrors?.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={`${itemError.item} ${itemError.statusText}`} />
                        </Grid>
                    ))}
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
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    serialNumbers.length > 0 && (
                        <Fragment>
                            <Grid item xs={3} className={classes.marginTop}>
                                <Dropdown
                                    value={selectedSernosGroup || ''}
                                    label="Filter by Sernos Group"
                                    fullWidth
                                    items={sernosGroups.length ? sernosGroups : ['']}
                                    onChange={handleFieldChange}
                                    propertyName="selectedSernosGroup"
                                />
                            </Grid>
                            <Grid item xs={9} />
                            {selectedSerialNumber && (
                                <Fragment>
                                    <SnackbarMessage
                                        visible={snackbarVisible}
                                        onClose={() => setSnackbarVisible(false)}
                                        message="Successfully reissued serial number"
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
                                                disabled={viewing() && !itemErrors}
                                                title="Sales Article Search"
                                                onSelect={handleNewArticleNumberSelect}
                                                searchItems={salesArticleSearchResults}
                                                loading={salesArticlesSearchLoading}
                                                fetchItems={searchSalesArticles}
                                                clearSearch={() => clearSalesArticlesSearch}
                                            />
                                        </div>
                                    </Grid>
                                    <Grid item xs={5}>
                                        <InputField
                                            disabled
                                            label="Description"
                                            type="string"
                                            propertyName="newArticleNumberDescription"
                                            value={selectedSerialNumber.newArticleNumberDescription}
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
                                            cancelClick={handleCancelClick}
                                            backClick={handleBackClick}
                                        />
                                    </Grid>
                                </Fragment>
                            )}
                        </Fragment>
                    )
                )}
                {!loading && !serialNumbersLoading && searchTerm && !serialNumbers.length && (
                    <Typography>{`Serial number ${searchTerm} not found`}</Typography>
                )}
            </Grid>
        </Page>
    );
}

SerialNumberReissue.propTypes = {
    addItem: PropTypes.func.isRequired,
    editStatus: PropTypes.string.isRequired,
    itemErrors: PropTypes.shape({}),
    fetchSerialNumbers: PropTypes.func.isRequired,
    fetchSalesArticle: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    salesArticle: PropTypes.shape({}),
    setEditStatus: PropTypes.func.isRequired,
    serialNumbers: PropTypes.arrayOf(PropTypes.shape()),
    serialNumbersLoading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    salesArticleSearchResults: PropTypes.arrayOf(PropTypes.shape()),
    salesArticlesSearchLoading: PropTypes.bool,
    clearSalesArticlesSearch: PropTypes.func.isRequired,
    searchSalesArticles: PropTypes.func.isRequired,
    reissuedSerialNumber: PropTypes.shape({}),
    options: PropTypes.shape({})
};

SerialNumberReissue.defaultProps = {
    itemErrors: null,
    loading: false,
    salesArticle: null,
    serialNumbers: [],
    serialNumbersLoading: false,
    snackbarVisible: false,
    salesArticleSearchResults: [],
    salesArticlesSearchLoading: false,
    reissuedSerialNumber: null,
    options: null
};

export default SerialNumberReissue;
