import {users as ActionTypes}  from "../constants/actionTypes";
import UserService from "../services/userService";
const userActions = {
    getUsers(searchParameters, callback) {
        return (dispatch) => {
            UserService.getUsers(searchParameters, payload => {
                dispatch({
                    type: ActionTypes.RETRIEVED_USERS,
                    payload
                });
                if (callback) {
                    callback(payload);
                }
            });
        };
    },
    getUserDetails(userDetailsParameters, callback) {
        return (dispatch) => {
            UserService.getUserDetails(userDetailsParameters, payload => {
                dispatch({
                    type: ActionTypes.RETRIEVED_USER_DETAILS,
                    payload
                });
                if (callback) {
                    callback(payload);
                }
            });
        };
    },
    getUserFilters(callback) {
        return () => {
            UserService.getUserFilters(payload => {
                // dispatch({
                //     type: ActionTypes.RETRIEVED_USER_FILTERS,
                //     payload
                // });
                if (callback) {
                    callback(payload);
                }
            });
        };
    },
    createUser(userDetails, callback) {
        return (dispatch) => {
            UserService.createUser(userDetails, payload => {
                dispatch({
                    type: ActionTypes.CREATE_USER,
                    payload
                });
                if (callback) {
                    callback(payload);
                }
            });
        };
    },
    updateUserBasicInfo(userDetails, callback) {
        return (dispatch) => {
            UserService.updateUserBasicInfo(userDetails, payload => {
                dispatch({
                    type: ActionTypes.UPDATE_USER,
                    payload
                });
                if (callback) {
                    callback(payload);
                }
            });
        };
    },
    changePassword(payload, callback) {
        return () => {
            UserService.changePassword(payload, data => {
                if (callback) {
                    callback(data);
                }
            });
        };
    },
    forceChangePassword(payload, callback) {
        return () => {
            UserService.forceChangePassword(payload, data => {
                if (callback) {
                    callback(data);
                }
            });
        };
    },
    sendPasswordResetLink(payload, callback) {
        return () => {
            UserService.sendPasswordResetLink(payload, data => {
                if (callback) {
                    callback(data);
                }
            });
        };
    },
    deleteUser(payload, callback) {
        return (dispatch) => {
            UserService.deleteUser(payload, data => {
                dispatch({
                    type: ActionTypes.DELETE_USER,
                    payload: { userId: payload.userId, Success: data.Success }
                });
                if (callback) {
                    callback(data);
                }
            });
        };
    },
    updateSuperUserStatus(payload, callback) {
        return (dispatch) => {
            UserService.updateSuperUserStatus(payload, data => {
                dispatch({
                    type: ActionTypes.USER_MADE_SUPERUSER,
                    payload: { userId: payload.userId, Success: data.Success }
                });
                if (callback) {
                    callback(data);
                }
            });
        };
    },
    updateAuthorizeStatus(payload, callback) {
        return () => {
            UserService.updateAuthorizeStatus(payload, data => {
                if (callback) {
                    callback(data);
                }
            });
        };
    }
};

export default userActions;
