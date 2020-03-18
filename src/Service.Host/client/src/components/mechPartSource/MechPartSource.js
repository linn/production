import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    InputField,
    SaveBackCancelButtons,
    Typeahead,
    SnackbarMessage,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

export default function MechPartSource({
    partsSearchResults,
    partsSearchLoading,
    itemErrors,
    partLoading,
    item,
    searchParts,
    clearPartsSearch,
    updatePart,
    partSnackbarVisible,
    setPartSnackbarVisible,
    history
}) {
    const [part, setPart] = useState(null);
    const [editing, setEditing] = useState(false);

    useEffect(() => {
        setPart(item);
    }, [item]);

    const handleFieldChange = (propertyName, newValue) => {
        setEditing(true);
        setPart({ ...part, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        updatePart(part.partNumber, part);
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditing(false);
    };

    const handleBackClick = () => {
        history.goBack();
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Mech Part Utility" />
                </Grid>

                {itemErrors &&
                    !partLoading &&
                    itemErrors?.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard
                                errorMessage={`${itemError.item} ${itemError.statusText} - ${itemError.details?.errors?.[0]}`}
                            />
                        </Grid>
                    ))}

                {partLoading ? (
                    <Loading />
                ) : (
                    <>
                        <SnackbarMessage
                            visible={partSnackbarVisible}
                            onClose={() => setPartSnackbarVisible(false)}
                            message="Save Succesful"
                        />
                        <Grid item xs={4}>
                            <Typeahead
                                onSelect={newValue => {
                                    setPart(newValue);
                                }}
                                label="Part"
                                modal
                                items={partsSearchResults}
                                value={part?.partNumber}
                                loading={partsSearchLoading}
                                fetchItems={searchParts}
                                links={false}
                                clearSearch={() => clearPartsSearch}
                                placeholder="Search by Part Number"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        {part && (
                            <>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={part.footprintRef}
                                        label="Footprint Ref"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="footprintRef"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={part.libraryRef}
                                        label="Library Ref"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="libraryRef"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={12}>
                                    <SaveBackCancelButtons
                                        saveDisabled={!editing}
                                        saveClick={handleSaveClick}
                                        cancelClick={handleCancelClick}
                                        backClick={handleBackClick}
                                    />
                                </Grid>
                            </>
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

MechPartSource.propTypes = {
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    partLoading: PropTypes.bool,
    item: PropTypes.shape({}),
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    updatePart: PropTypes.func.isRequired,
    partSnackbarVisible: PropTypes.bool,
    setPartSnackbarVisible: PropTypes.func.isRequired
};

MechPartSource.defaultProps = {
    partsSearchResults: null,
    partsSearchLoading: false,
    itemErrors: null,
    partLoading: false,
    item: null,
    partSnackbarVisible: false
};
