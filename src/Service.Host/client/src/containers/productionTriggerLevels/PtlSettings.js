import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import PtlSettings from '../../components/productionTriggerLevels/PtlSettings';
import ptlSettingsActions from '../../actions/ptlSettingsActions';
import ptlSettingsSelectors from '../../selectors/ptlSettingsSelectors';
import initialiseOnMount from '../initialiseOnMount';

const mapStateToProps = state => ({
    item: ptlSettingsSelectors.getItem(state),
    editStatus: ptlSettingsSelectors.getEditStatus(state),
    loading: ptlSettingsSelectors.getLoading(state),
    snackbarVisible: ptlSettingsSelectors.getSnackbarVisible(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = () => dispatch => {
    dispatch(ptlSettingsActions.fetch(''));
};

const mapDispatchToProps = {
    initialise,
    updateItem: ptlSettingsActions.update,
    setEditStatus: ptlSettingsActions.setEditStatus,
    setSnackbarVisible: ptlSettingsActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(PtlSettings));
