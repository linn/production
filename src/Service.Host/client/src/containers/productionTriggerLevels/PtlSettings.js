import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import PtlSettings from '../../components/productionTriggerLevels/PtlSettings';
import ptlSettingsActions from '../../actions/ptlSettingsActions';
import ptlSettingsSelectors from '../../selectors/ptlSettingsSelectors';
import initialiseOnMount from '../initialiseOnMount';
import startTriggerRunActions from '../../actions/startTriggerRunActions';
import startTriggerRunSelectors from '../../selectors/startTriggerRunSelectors';

const mapStateToProps = state => ({
    item: ptlSettingsSelectors.getItem(state),
    editStatus: ptlSettingsSelectors.getEditStatus(state),
    loading: ptlSettingsSelectors.getLoading(state),
    snackbarVisible: ptlSettingsSelectors.getSnackbarVisible(state),
    startTriggerRunMessageVisible: startTriggerRunSelectors.getMessageVisible(state),
    errorMessage: fetchErrorSelectors(state),
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
