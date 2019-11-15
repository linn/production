import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import PtlSettings from '../../components/productionTriggerLevels/PtlSettings';
import ptlSettingsActions from '../../actions/ptlSettingsActions';
import ptlSettingsSelectors from '../../selectors/ptlSettingsSelectors';
import startTriggerRunActions from '../../actions/startTriggerRunActions';
import startTriggerRunSelectors from '../../selectors/startTriggerRunSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: ptlSettingsSelectors.getItem(state),
    editStatus: ptlSettingsSelectors.getEditStatus(state),
    loading: ptlSettingsSelectors.getLoading(state),
    snackbarVisible: ptlSettingsSelectors.getSnackbarVisible(state),
    startTriggerRunMessageVisible: startTriggerRunSelectors.getMessageVisible(state),
    getItemError: getItemError(state, itemTypes.ptlSettings),
    startTriggerRunMessageText: startTriggerRunSelectors.getMessageText(state)
});

const initialise = () => dispatch => {
    dispatch(ptlSettingsActions.fetch(''));
};

const mapDispatchToProps = {
    initialise,
    updateItem: ptlSettingsActions.update,
    setEditStatus: ptlSettingsActions.setEditStatus,
    setSnackbarVisible: ptlSettingsActions.setSnackbarVisible,
    setStartTriggerRunMessageVisible: startTriggerRunActions.setMessageVisible,
    startTriggerRun: startTriggerRunActions.requestProcessStart
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(PtlSettings));
