import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import mechPartSourceActions from '../../actions/mechPartSourceActions';
import mechPartSourceSelectors from '../../selectors/mechPartSourceSelectors';
import MechPartSource from '../../components/mechPartSource/MechPartSource';

const mapStateToProps = state => ({
    loading: mechPartSourceSelectors.getLoading(state),
    item: mechPartSourceSelectors.getItem(state),
    snackbarVisible: mechPartSourceSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    editStatus: mechPartSourceSelectors.getEditStatus(state)
});

const mapDispatchToProps = {
    fetchMechPartSource: mechPartSourceActions.fetch,
    updateMechPartSource: mechPartSourceActions.update,
    setSnackbarVisible: mechPartSourceActions.setSnackbarVisible,
    setEditStatus: mechPartSourceActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(MechPartSource);
