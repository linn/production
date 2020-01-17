import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { ateTestActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.ateTest.item,
    itemTypes.ateTest.actionType,
    itemTypes.ateTest.uri,
    actionTypes,
    config.appRoot
);
