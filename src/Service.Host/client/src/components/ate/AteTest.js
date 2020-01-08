import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AteTest({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible
}) {
    const [ateTest, setAteTest] = useState({});
    const [prevAteTest, setPrevAteTest] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevAteTest) {
            setAteTest(item);
            setPrevAteTest(item);
        }
    }, [item, prevAteTest]);

    const faultCodeInvalid = () => !ateTest.faultCode;
    const descriptionInvalid = () => !ateTest.description;

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, ateTest);
            setEditStatus('view');
        } else if (creating()) {
            addItem(ateTest);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setAteTest(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        // history.push('');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAteTest({ ...ateTest, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create ATE Fault Code" />
                    ) : (
                        <Title text="ATE Fault Code" />
                    )}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    ateTest &&
                    itemError?.faultCode !== 404 && (
                        <Fragment>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={ateTest.testId}
                                    label="Id"
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="testId"
                                />
                            </Grid>
                            
                        </Fragment>
                    )
                )}
            </Grid>
        </Page>
    );
}

AteTest.propTypes = {
    item: PropTypes.shape({
        ateTest: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

AteTest.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default AteTest;
